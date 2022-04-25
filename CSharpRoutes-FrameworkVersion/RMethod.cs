using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoutes_FrameworkVersion
{
    public class RMethod
    {

        /// <summary>
        /// Metodo Get, sirve para obtener datos de una ruta especifica
        /// </summary>
        /// <param name="stringruta">
        /// Recibe la ruta ya establecida
        /// </param>
        /// <param name="json">
        /// Recibe el formato Json para hacer evaluaciones
        /// </param>
        /// <returns name="Object">
        /// si hay una respuesta o todo esta correcto devolvera un objeto dependiendo de su backend, sino devolvera false
        /// </returns>
        public static object Get(string stringruta, string json = null)
        {
            if (!string.IsNullOrWhiteSpace(stringruta))
            {
                if (Rutas.IsRouteExits(Metodos.MGet, stringruta))
                {
                    if (json == null)
                    {
                        ReponseRoutesNotice response = new ReponseRoutesNotice();
                        response.Resultado = true;
                        response.Mensaje = "json nulo";
                        response.Data = Rutas.Get().Find(e => e.ruta == stringruta).Accion.Invoke();
                        return JsonConvert.SerializeObject(response);
                    }
                    else
                    {

                        var mid = (ReponseRoutesNotice)Rutas.Get().Find(e => e.ruta == stringruta).Middleware.Invoke(json);
                        if (mid.Resultado == true)
                        {
                            ReponseRoutesNotice response = new ReponseRoutesNotice();
                            response.Mensaje = mid.Mensaje;
                            response.Resultado = mid.Resultado;
                            response.Data = Rutas.Get().Find(e => e.ruta == stringruta).AccionFull.Invoke(json);
                            return JsonConvert.SerializeObject(response);
                        }
                        else
                            return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = mid.Mensaje });
                    }
                }
                else
                    return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "Esa Ruta No Existe" });
            }
            else
                return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "La ruta" });
        }

        /// <summary>
        /// Metodo Post, sirve para Enviar Datos a una ruta especifica
        /// </summary>
        /// <param name="stringruta">
        /// Recibe la ruta ya establecida
        /// </param>
        /// <param name="json">
        /// Recibe el formato Json para insertar
        /// </param>
        /// <returns name="Object">
        /// si hay una respuesta o todo esta correcto devolvera un objeto dependiendo de su backend, sino devolvera false
        /// </returns>
        public static object Post(string stringruta, string json)
        {
            if (!string.IsNullOrWhiteSpace(stringruta) && !string.IsNullOrWhiteSpace(json))
            {
                if (Rutas.IsRouteExits(Metodos.MPost, stringruta))
                {
                    var mid = (ReponseRoutesNotice)Rutas.Post().Find(e => e.ruta == stringruta).Middleware.Invoke(json);
                    if (mid.Resultado == true)
                    {
                        ReponseRoutesNotice response = new ReponseRoutesNotice();
                        response.Mensaje = mid.Mensaje;
                        response.Resultado = mid.Resultado;
                        response.Data = Rutas.Post().Find(e => e.ruta == stringruta).Accion.Invoke(json);
                        return JsonConvert.SerializeObject(response);
                    }
                    else
                        return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = mid.Mensaje });
                }
                else
                    return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "Esa Ruta No Existe" });
            }
            else
                return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "La ruta o el json estan vacios" });
        }
        /// <summary>
        /// Metodo Put, sirve para modificar algun dato
        /// </summary>
        /// <param name="stringruta">
        /// Recibe la ruta ya establecida
        /// </param>
        /// <param name="json">
        /// Recibe el formato Json para actualizar los datos
        /// </param>
        /// <returns name="Object">
        /// si hay una respuesta o todo esta correcto devolvera un objeto dependiendo de su backend, sino devolvera false
        /// </returns>
        public static object Put(string stringruta, string json)
        {
            if (!string.IsNullOrWhiteSpace(stringruta) && !string.IsNullOrWhiteSpace(json))
            {
                if (Rutas.IsRouteExits(Metodos.MPut, stringruta))
                {
                    var mid = (ReponseRoutesNotice)Rutas.Put().Find(e => e.ruta == stringruta).Middleware.Invoke(json);
                    if (mid.Resultado == true)
                    {
                        ReponseRoutesNotice response = new ReponseRoutesNotice();
                        response.Mensaje = mid.Mensaje;
                        response.Resultado = mid.Resultado;
                        response.Data = Rutas.Put().Find(e => e.ruta == stringruta).Accion.Invoke(json);
                        return JsonConvert.SerializeObject(response);
                    }
                    else
                        return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = mid.Mensaje });
                }
                else
                    return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "Esa Ruta No Existe" });
            }
            else
                return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "La ruta o el json estan vacios" });

        }

        /// <summary>
        /// Metodo delete, sirve para eliminar algun dato
        /// </summary>
        /// <param name="stringruta">
        /// Recibe la ruta ya establecida
        /// </param>
        /// <param name="json">
        /// Recibe el formato Json para eliminar datos
        /// </param>
        /// <returns name="Object">
        /// si hay una respuesta o todo esta correcto devolvera un objeto dependiendo de su backend, sino devolvera false
        /// </returns>
        public static object Delete(string stringruta, string json)
        {
            if (!string.IsNullOrWhiteSpace(stringruta) && !string.IsNullOrWhiteSpace(json))
            {
                if (Rutas.IsRouteExits(Metodos.MDel, stringruta))
                {
                    var mid = (ReponseRoutesNotice)Rutas.Delete().Find(e => e.ruta == stringruta).Middleware.Invoke(json);
                    if (mid.Resultado == true)
                    {
                        ReponseRoutesNotice response = new ReponseRoutesNotice();
                        response.Mensaje = mid.Mensaje;
                        response.Resultado = mid.Resultado;
                        response.Data = Rutas.Delete().Find(e => e.ruta == stringruta).Accion.Invoke(json);
                        return JsonConvert.SerializeObject(response);
                    }
                    else
                        return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = mid.Mensaje });
                }
                else
                    return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "Esa Ruta No Existe" });
            }
            else
                return JsonConvert.SerializeObject(new ReponseRoutesNotice() { Resultado = false, Mensaje = "La ruta o el json estan vacios" });
        }

        public static object GetData(object json)
        {
            return JObject.Parse((string)json).GetValue("Data").ToString();
        }

    }


}
