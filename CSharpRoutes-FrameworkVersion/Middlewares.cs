using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSharpRoutes.lib
{
    public class Middlewares
    {

        ReponseRoutesNotice _result = new ReponseRoutesNotice();

        /// <summary>
        /// Obtiene el Resultado de las comparaciones, si algun resultado es falso devuelve el resultado y deja de comprobar
        /// </summary>
        /// <returns name="ReponseRoutesNotice">
        /// ResponseRoutesNotice: contiene resultado (bool), Mensaje (string), Data (Object)
        /// </returns>
        public ReponseRoutesNotice Next()
        {
            return _result;
        }

        void change(ReponseRoutesNotice r)
        {
            _result = r;
        }
        bool verificar()
        {
            return Next().Resultado;
        }

        /// <summary>
        /// Verifica si se trata de un Json o no
        /// </summary>
        /// <param name="json">
        /// El json a evaluar
        /// </param>
        /// <returns>
        /// Retorna un Middleware para continuar con next()
        /// </returns>
        public Middlewares IsJson(object json)
        {
            if (verificar())
            {
                try
                {
                    JObject jo = JObject.Parse((string)json);
                }
                catch (Exception)
                {
                    _result.Resultado = false;
                    _result.Mensaje = "Esto no es un Json";
                }
                change(_result);
            }
            return this;
        }

        /// <summary>
        /// Verifica si los campos esta completos
        /// </summary>
        /// <param name="json">
        /// Json a Evaluar
        /// </param>
        /// <param name="campos">
        /// los campos que tiene el json
        /// </param>
        /// <returns>
        /// Middleware para continuar con next()
        /// </returns>
        public Middlewares IsComplete(object json, string[] campos)
        {
            if (verificar())
            {
                if (!string.IsNullOrWhiteSpace((string)json) && campos.Length > 0)
                {
                    JObject jobj = JObject.Parse((string)json);
                    bool a = true;
                    foreach (var campo in campos)
                    {
                        if (jobj.GetValue(campo) == null)
                        {
                            a = false;
                            break;
                        }
                    }
                    if (a != true)
                    {
                        _result.Resultado = false;
                        _result.Mensaje = "El Json No esta Completo";
                    }
                }
                else
                {
                    _result.Resultado = false; _result.Mensaje = "Los parametros estaban vacios";
                }
                change(_result);
            }

            return this;
        }

        /// <summary>
        /// Verifica si un campo es Boolean
        /// </summary>
        /// <param name="json">
        /// Json a evaluar
        /// </param>
        /// <param name="Campo">
        /// Es campo a evaluar dentro del json
        /// </param>
        /// <returns>
        /// Middleware para continuar con next()
        /// </returns>
        public Middlewares IsBool(object json, string Campo)
        {

            if (verificar())
            {
                if (!string.IsNullOrWhiteSpace((string)json) && !string.IsNullOrWhiteSpace(Campo))
                {
                    JToken valor = JObject.Parse((string)json).GetValue(Campo);
                    if (valor.Type == JTokenType.Boolean == false)
                    { _result.Resultado = false; _result.Mensaje = "No es Boleano"; }
                }
                else
                {
                    _result.Resultado = false; _result.Mensaje = "Los parametros estaban vacios";
                }
                change(_result);
            }
            return this;
        }

        /// <summary>
        /// Verificar si un campo es un entero
        /// </summary>
        /// <param name="json">
        /// Json a evaluar
        /// </param>
        /// <param name="Campo">
        /// El campos a verificar dentro del json
        /// </param>
        /// <returns>
        /// Middleware para continuar con next()
        /// </returns>
        public Middlewares IsInt(object json, string Campo)
        {
            if (verificar())
            {
                if (!string.IsNullOrWhiteSpace((string)json) && !string.IsNullOrWhiteSpace(Campo))
                {
                    JToken valor = JObject.Parse((string)json).GetValue(Campo);
                    if (valor.Type == JTokenType.Integer == false)
                    { _result.Resultado = false; _result.Mensaje = "No es Entero"; }
                }
                else
                {
                    _result.Resultado = false; _result.Mensaje = "Los parametros estaban vacios";
                }
                change(_result);
            }

            return this;
        }

        /// <summary>
        /// Verifica si un campo es String
        /// </summary>
        /// <param name="json">
        /// Json a evaluar
        /// </param>
        /// <param name="campo">
        /// Campo a evaluar dentro del json
        /// </param>
        /// <returns>
        /// Middleware para continuar con next()
        /// </returns>
        public Middlewares IsString(object json, string campo)
        {
            if (verificar())
            {
                if (!string.IsNullOrWhiteSpace((string)json) && !string.IsNullOrWhiteSpace(campo))
                {
                    JToken valor = JObject.Parse((string)json).GetValue(campo);
                    if (valor.Type == JTokenType.String == false)
                    { _result.Resultado = false; _result.Mensaje = "No es String"; }
                }
                else
                { _result.Resultado = false; _result.Mensaje = "Los parametros estaban vacios"; }
                change(_result);
            }

            return this;
        }

        /// <summary>
        /// Verifica si un campo no es nulo
        /// </summary>
        /// <param name="json">
        /// Json a evaluar
        /// </param>
        /// <param name="campo">
        /// El campo a evaluar dentro del json
        /// </param>
        /// <returns>
        /// Middleware para continuar con next()
        /// </returns>
        public Middlewares IsNotNull(object json, string campo)
        {
            if (verificar())
            {
                if (!string.IsNullOrWhiteSpace((string)json) && !string.IsNullOrWhiteSpace(campo))
                {
                    JToken valor = JObject.Parse((string)json).GetValue(campo);
                    if (valor.Type == JTokenType.Null == true)
                    { _result.Resultado = false; _result.Mensaje = "Es Nulo"; }
                }
                else
                { _result.Resultado = false; _result.Mensaje = "Los parametros estaban vacios"; }
                change(_result);
            }
            return this;
        }

        /// <summary>
        /// Verificacion personalizada realizada por el programador
        /// </summary>
        /// <param name="json">
        /// Json a evaluar
        /// </param>
        /// <param name="func">
        /// Funcion que recibe el json y retorna un ResponseRoutesNotice
        /// </param>
        /// <returns>
        /// Middleware para continuar con next()
        /// </returns>
        public Middlewares IsPersonalizado(object json, Func<object, ReponseRoutesNotice> func)
        {
            if (verificar())
            {
                if (json != null && func != null)
                {
                    var r = func.Invoke(json);
                    if (r.Resultado == false)
                    {
                        _result = r;
                    }
                }
                else
                { _result.Resultado = false; _result.Mensaje = "Los parametros estaban vacios"; }
                change(_result);
            }
            return this;
        }

    }
}
