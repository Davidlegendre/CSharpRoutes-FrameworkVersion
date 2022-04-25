using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpRoutes.lib
{
    public class Rutas
    {
        static List<Post> _Insert = new List<Post>();
        static List<Put> _Update = new List<Put>();
        static List<Delete> _Delete = new List<Delete>();
        static List<Get> _Select = new List<Get>();
        static List<Post> RPost { get => _Insert; set => _Insert = value; }
        static List<Put> RPut { get => _Update; set => _Update = value; }
        static List<Get> RGet { get => _Select; set => _Select = value; }
        static List<Delete> RDelete { get => _Delete; set => _Delete = value; }

        /// <summary>
        /// Verifica si una ruta existe
        /// </summary>
        /// <param name="metodo">
        /// Recibe un Metodos.MGet | MPost | MPut | MDel, para saber donde buscar
        /// </param>
        /// <param name="ruta">
        /// la ruta a evaluar
        /// </param>
        /// <returns>
        /// retorna un bool, true si existe y falso si no
        /// </returns>
        public static bool IsRouteExits(int metodo, string ruta)
        {
            switch (metodo)
            {
                case Metodos.MGet:
                    return Get().Count(e => e.ruta.ToLower() == ruta.ToLower()) != 0;
                case Metodos.MPost:
                    return Post().Count(e => e.ruta.ToLower() == ruta.ToLower()) != 0;
                case Metodos.MPut:
                    return Put().Count(e => e.ruta.ToLower() == ruta.ToLower()) != 0;
                case Metodos.MDel:
                    return Delete().Count(e => e.ruta.ToLower() == ruta.ToLower()) != 0;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Agrega una ruta de tipo Post a la lista
        /// </summary>
        /// <param name="postRuta">
        /// Objeto de Tipo Post
        /// </param>
        public static void Post(Post postRuta)
        {
            new IComparer(Metodos.MPost, postRuta.ruta);
            new IComparer(postRuta);
            RPost.Add(postRuta);
        }

        /// <summary>
        /// Agrega una ruta de tipo Get a la lista
        /// </summary>
        /// <param name="getRuta">
        /// Objeto de Tipo Get
        /// </param>
        public static void Get(Get getRuta)
        {
            new IComparer(Metodos.MGet, getRuta.ruta);
            new IComparer(getRuta);
            RGet.Add(getRuta);
        }

        /// <summary>
        /// Agrega una ruta de tipo Put a la lista
        /// </summary>
        /// <param name="putRuta">
        /// Objeto de Tipo Put
        /// </param>
        public static void Put(Put putRuta)
        {
            new IComparer(Metodos.MPut, putRuta.ruta);
            new IComparer(putRuta);
            RPut.Add(putRuta);

        }

        /// <summary>
        /// Agrega una ruta de tipo Delete a la lista
        /// </summary>
        /// <param name="deleteRuta">
        /// Objeto de Tipo Delete
        /// </param>
        public static void Delete(Delete deleteRuta)
        {
            new IComparer(Metodos.MDel, deleteRuta.ruta);
            new IComparer(deleteRuta);
            RDelete.Add(deleteRuta);
        }

        /// <summary>
        /// Obtiene la lista completa de las rutas Post
        /// </summary>
        public static List<Post> Post()
        {
            return RPost;
        }

        /// <summary>
        /// Obtiene la lista completa de las rutas Get
        /// </summary>
        public static List<Get> Get()
        {
            return RGet;
        }


        /// <summary>
        /// Obtiene la lista completa de las rutas Put
        /// </summary>
        public static List<Put> Put()
        {
            return RPut;
        }

        /// <summary>
        /// Obtiene la lista completa de las rutas Delete
        /// </summary>
        public static List<Delete> Delete()
        {
            return RDelete;
        }

    }

    /// <summary>
    /// Objeto Post, Contiene Ruta (string), Accion (Func<in object, out object>) y Middleware (Func<in object, out bool>)
    /// </summary>
    public partial class Post
    {
        string _ruta = "";
        Func<object, object> _Accion = null;
        Func<object, object> _Middleware = null;

        /// <summary>
        /// La ruta a donde se navegara
        /// </summary>
        public string ruta { get => _ruta; set => _ruta = value; }

        /// <summary>
        /// Accion que se ejecutara una vez pasado el Middleware
        /// obtiene un json o un objeto y devuelve un objeto
        /// </summary>
        public Func<object, object> Accion { get => _Accion; set => _Accion = value; }

        /// <summary>
        /// El Middleware que evalua segun lo que usted desee al json
        /// obtiene un json o un objeto y devuelve un objeto
        /// </summary>
        public Func<object, object> Middleware { get => _Middleware; set => _Middleware = value; }
    }

    /// <summary>
    /// Objeto Put, Contiene: Ruta (string), Accion (Func<in object, out object>) y Middleware (Func<in object, out object>)
    /// </summary>
    public partial class Put
    {
        string _ruta = "";
        Func<object, object> _Accion = null;
        Func<object, object> _Middleware = null;

        /// <summary>
        /// La ruta a donde se navegara
        /// </summary>
        public string ruta { get => _ruta; set => _ruta = value; }

        /// <summary>
        /// Accion que se ejecutara una vez pasado el Middleware
        /// </summary>
        public Func<object, object> Accion { get => _Accion; set => _Accion = value; }

        /// <summary>
        /// El Middleware que evalua segun lo que usted desee al json
        /// obtiene un json o un objeto y devuelve un objeto
        /// </summary>
        public Func<object, object> Middleware { get => _Middleware; set => _Middleware = value; }

    }

    /// <summary>
    /// Objeto Get, Contiene: Ruta (string), Accion (Func<out object>), AccionFull (Func<in object, out object>) 
    /// y Middleware (Func<in string, out bool>).
    /// </summary>
    public partial class Get
    {

        string _ruta = "";
        Func<object> _Accion = null;
        Func<object, object> _Middleware = null;
        string[] _campos = new string[1];
        Func<object, object> _AccionConFuncion = null;

        /// <summary>
        /// La ruta a donde se navegara
        /// </summary>
        public string ruta { get => _ruta; set => _ruta = value; }

        /// <summary>
        /// Accion que se ejecutara si no se va a enviar json
        /// </summary>
        public Func<object> Accion { get => _Accion; set => _Accion = value; }

        /// <summary>
        /// Accion que se ejecutara cuando se quiera enviar jsons u otro objeto
        /// </summary>
        public Func<object, object> AccionFull { get => _AccionConFuncion; set => _AccionConFuncion = value; }

        /// <summary>
        /// El Middleware que evalua segun lo que usted desee al json
        /// obtiene un json o un objeto y devuelve un objeto
        /// </summary>
        public Func<object, object> Middleware { get => _Middleware; set => _Middleware = value; }

    }

    /// <summary>
    /// Objeto Delete, Contiene: Ruta (string), Accion (Func<in object, out object>) y Middleware (Func<in object, out bool>)
    /// </summary>
    public partial class Delete
    {

        string _ruta = "";
        Func<object, object> _AccionConFuncion = null;

        Func<object, object> _Middleware = null;

        /// <summary>
        /// La ruta a donde se navegara
        /// </summary>
        public string ruta { get => _ruta; set => _ruta = value; }

        /// <summary>
        /// Accion que se ejecutara una vez pasado el Middleware
        /// obtiene un json o un objeto y devuelve un objeto
        /// </summary>
        public Func<object, object> Accion { get => _AccionConFuncion; set => _AccionConFuncion = value; }

        /// <summary>
        /// El Middleware que evalua segun lo que usted desee al json
        /// obtiene un json o un objeto y devuelve un objeto
        /// </summary>
        public Func<object, object> Middleware { get => _Middleware; set => _Middleware = value; }


    }

    /// <summary>
    /// MGet = 1;\n
    /// MPost = 2;\n
    /// MPut = 3;\n
    /// MDel = 4;
    /// </summary>
    public static partial class Metodos
    {
        public const int MGet = 1;
        public const int MPost = 2;
        public const int MPut = 3;
        public const int MDel = 4;
    }

    /// <summary>
    /// Hace casi lo mismo que IsRouteExits() con la diferencia de que genera excepciones si hay errores
    /// </summary>
    internal class IComparer
    {
        /// <summary>
        /// Verifica si una ruta existe
        /// </summary>
        /// <param name="Mnum">
        /// Recibe un Metodos.MGet | MPost | MPut | MDel, para saber donde buscar
        /// </param>
        /// <param name="ruta">
        /// La ruta a evaluar
        /// </param>
        /// <exception cref="Exception">
        /// Provoca una excepcion si hay error
        /// </exception>
        public IComparer(int Mnum, string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new Exception("Algunas de sus rutas estan vacias");
            int countr = 0;
            switch (Mnum)
            {
                case Metodos.MGet:
                    countr = RGet.Count(e => e.ruta.ToLower() == ruta.ToLower());
                    if (countr != 0)
                        throw new Exception("La Ruta: " + ruta + " Ya existe");

                    break;
                case Metodos.MPost:
                    countr = RPost.Count(e => e.ruta.ToLower() == ruta.ToLower());
                    if (countr != 0)
                        throw new Exception("La Ruta: " + ruta + " Ya existe");
                    break;
                case Metodos.MPut:

                    countr = RPut.Count(e => e.ruta.ToLower() == ruta.ToLower());
                    if (countr != 0)
                        throw new Exception("La Ruta: " + ruta + " Ya existe");

                    break;
                case Metodos.MDel:
                    countr = RDelete.Count(e => e.ruta.ToLower() == ruta.ToLower());
                    if (countr != 0)
                        throw new Exception("La Ruta: " + ruta + " Ya existe");
                    break;
            }


        }

        /// <summary>
        /// Verifica la integridad de la ruta
        /// </summary>
        /// <param name="ruta">
        /// ruta tipo objeto Get
        /// </param>
        /// <exception cref="Exception">
        /// Excepcion cuando hay errrores
        /// </exception>
        public IComparer(Get ruta)
        {
            if (ruta.Accion == null && ruta.AccionFull == null)
                throw new Exception("Añada una Accion a la ruta " + ruta.ruta);
            else if (ruta.Accion != null && ruta.AccionFull != null)
                throw new Exception("Elija Una Sola Accion, ruta: " + ruta.ruta);
            else if (ruta.Middleware == null && ruta.AccionFull != null)
                throw new Exception("Añada un Middleware a la ruta " + ruta.ruta);

        }

        /// <summary>
        /// Verifica la integridad de la ruta
        /// </summary>
        /// <param name="ruta">
        /// ruta tipo objeto Post
        /// </param>
        /// <exception cref="Exception">
        /// Excepcion cuando hay errrores
        /// </exception>
        public IComparer(Post ruta)
        {
            if (ruta.Accion == null)
                throw new Exception("Añada una Accion a la ruta " + ruta.ruta);
            else if (ruta.Middleware == null && ruta.Accion != null)
                throw new Exception("Añada un Middleware a la ruta " + ruta.ruta);
        }

        /// <summary>
        /// Verifica la integridad de la ruta
        /// </summary>
        /// <param name="ruta">
        /// ruta tipo objeto Put
        /// </param>
        /// <exception cref="Exception">
        /// Excepcion cuando hay errrores
        /// </exception>
        public IComparer(Put ruta)
        {
            if (ruta.Accion == null)
                throw new Exception("Añada una Accion a la ruta " + ruta.ruta);
            else if (ruta.Middleware == null && ruta.Accion != null)
                throw new Exception("Añada un Middleware a la ruta " + ruta.ruta);
        }

        /// <summary>
        /// Verifica la integridad de la ruta
        /// </summary>
        /// <param name="ruta">
        /// ruta tipo objeto Delete
        /// </param>
        /// <exception cref="Exception">
        /// Excepcion cuando hay errrores
        /// </exception>
        public IComparer(Delete ruta)
        {
            if (ruta.Accion == null)
                throw new Exception("Añada una Accion a la ruta " + ruta.ruta);
            else if (ruta.Middleware == null && ruta.Accion != null)
                throw new Exception("Añada un Middleware a la ruta " + ruta.ruta);
        }
        List<Get> RGet = Rutas.Get();
        List<Post> RPost = Rutas.Post();
        List<Put> RPut = Rutas.Put();
        List<Delete> RDelete = Rutas.Delete();

    }
}
