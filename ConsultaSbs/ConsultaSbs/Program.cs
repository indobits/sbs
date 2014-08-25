using System;

using HtmlAgilityPack;

namespace ConsultaSbs
{

    public class Program
    {

        public class Colaborador
        {

            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string PrimerNombre { get; set; }
            public string SegundoNombre { get; set; }
            public string TipoTrabajador { get; set; }
            public string Sexo { get; set; }
            public string Nacionalidad { get; set; }
            public string LugarNacimiento { get; set; }
            public string LugarResidencia { get; set; }
            public string EstadoCivil { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public DateTime? FechaDefuncion { get; set; }
            public DateTime? FechaProcesoDefuncion { get; set; }
            public string OrigenAfiliacion { get; set; }
            public string EntidadAfiliacion { get; set; }
            public string TipoComisionAfiliacion { get; set; }
            public string CodigoAfiliacion { get; set; }
            public DateTime FechaIngresoAfiliacion { get; set; }
            public string SituacionAfiliacion { get; set; }

            public Colaborador()
            {



            }

            public Colaborador(

                string apellidoPaterno,
                string apellidoMaterno,
                string primerNombre,
                string segundoNombre,
                string tipoTrabajador,
                string sexo,
                string nacionalidad,
                string lugarNacimiento,
                string lugarResidencia,
                string estadoCivil,
                DateTime fechaNacimiento,
                DateTime? fechaDefuncion,
                DateTime? fechaProcesoDefuncion,
                string origenAfiliacion,
                string entidadAfiliacion,
                string tipoComisionAfiliacion,
                string codigoAfiliacion,
                DateTime fechaIngresoAfiliacion,
                string situacionAfiliacion

            )
            {

                ApellidoPaterno = apellidoPaterno;
                ApellidoMaterno = apellidoMaterno;
                PrimerNombre = primerNombre;
                SegundoNombre = segundoNombre;
                TipoTrabajador = tipoTrabajador;
                Sexo = sexo;
                Nacionalidad = nacionalidad;
                LugarNacimiento = lugarNacimiento;
                LugarResidencia = lugarResidencia;
                EstadoCivil = estadoCivil;
                FechaNacimiento = fechaNacimiento;
                FechaDefuncion = fechaDefuncion;
                FechaProcesoDefuncion = fechaProcesoDefuncion;
                OrigenAfiliacion = origenAfiliacion;
                EntidadAfiliacion = entidadAfiliacion;
                TipoComisionAfiliacion = tipoComisionAfiliacion;
                CodigoAfiliacion = codigoAfiliacion;
                FechaIngresoAfiliacion = fechaIngresoAfiliacion;
                SituacionAfiliacion = situacionAfiliacion;

            }

        }

        public static Colaborador ConsultaSbs(string tipoDocumento, string numeroDocumento)
        {

            switch (tipoDocumento)
            {

                case "01":
                    tipoDocumento = "00";
                    break;
                case "04":
                    tipoDocumento = "01";
                    break;
                default:
                    Console.WriteLine(@"Tipo de Documento desconocido");
                    break;

            }

            var web = new HtmlWeb();
            var url = @"http://www.sbs.gob.pe/app/spp/Afiliados/afil_detalle.asp?tp=2&tip_doc=" + tipoDocumento + "&num_doc=" + numeroDocumento;
            var document = web.Load(url);

            var nodeCollection = document.DocumentNode.SelectNodes("//td");

            var existe = nodeCollection[0].InnerText.Replace("&nbsp;", "");

            if (existe == "Documento de Identidad no registrado en el SPP.")
                return null;

            var apellidoPaterno = nodeCollection[6].InnerText.Replace("&nbsp;", "");
            var apellidoMaterno = nodeCollection[8].InnerText.Replace("&nbsp;", "");
            var primerNombre = nodeCollection[11].InnerText.Replace("&nbsp;", "");
            var segundoNombre = nodeCollection[13].InnerText.Replace("&nbsp;", "");
            var tipoTrabajador = nodeCollection[31].InnerText.Replace("&nbsp;", "");
            var sexo = nodeCollection[17].InnerText.Replace("&nbsp;", "").Substring(5, nodeCollection[17].InnerText.Replace("&nbsp;", "").Length - 5);
            var nacionalidad = nodeCollection[23].InnerText.Replace("&nbsp;", "");
            var lugarNacimiento = nodeCollection[21].InnerText.Replace("&nbsp;", "");
            var lugarResidencia = nodeCollection[26].InnerText.Replace("&nbsp;", "");
            var estadoCivil = nodeCollection[19].InnerText.Replace("&nbsp;", "");
            var fechaNacimiento = Convert.ToDateTime(nodeCollection[16].InnerText.Replace("&nbsp;", ""));
            var fechaDefuncion = (nodeCollection[45].InnerText.Replace("&nbsp;", "") == string.Empty) ? (DateTime?)null : Convert.ToDateTime(nodeCollection[45].InnerText.Replace("&nbsp;", ""));
            var fechaProcesoDefuncion = (nodeCollection[47].InnerText.Replace("&nbsp;", "") == string.Empty) ? (DateTime?)null : Convert.ToDateTime(nodeCollection[47].InnerText.Replace("&nbsp;", ""));
            var origenAfiliacion = nodeCollection[29].InnerText.Replace("&nbsp;", "");
            var entidadAfiliacion = nodeCollection[35].InnerText.Replace("&nbsp;", "");
            var tipoComisionAfiliacion = nodeCollection[42].InnerText.Replace("&nbsp;", "");
            var codigoAfiliacion = nodeCollection[33].InnerText.Replace("&nbsp;", "");
            var fechaIngresoAfiliacion = Convert.ToDateTime(nodeCollection[37].InnerText.Replace("&nbsp;", ""));
            var situacionAfiliacion = nodeCollection[40].InnerText.Replace("&nbsp;", "");

            var colaborador = new Colaborador
            {

                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                PrimerNombre = primerNombre,
                SegundoNombre = segundoNombre,
                TipoTrabajador = tipoTrabajador,
                Sexo = sexo,
                Nacionalidad = nacionalidad,
                LugarNacimiento = lugarNacimiento,
                LugarResidencia = lugarResidencia,
                EstadoCivil = estadoCivil,
                FechaNacimiento = fechaNacimiento,
                FechaDefuncion = fechaDefuncion,
                FechaProcesoDefuncion = fechaProcesoDefuncion,
                OrigenAfiliacion = origenAfiliacion,
                EntidadAfiliacion = entidadAfiliacion,
                TipoComisionAfiliacion = tipoComisionAfiliacion,
                CodigoAfiliacion = codigoAfiliacion,
                FechaIngresoAfiliacion = fechaIngresoAfiliacion,
                SituacionAfiliacion = situacionAfiliacion,

            };

            return colaborador;

        }

        public static void Main(string[] args)
        {

            Console.WriteLine();
            Console.WriteLine("Sistema de Consulta a la Sbs");
            Console.WriteLine("============================");
            Console.WriteLine();

            string tipoDocumento;
            var flagTipoDocumento = false;

            do
            {

                Console.Write("Ingrese el Tipo de Documento: ");
                tipoDocumento = Console.ReadLine();

                if (tipoDocumento == "01" || tipoDocumento == "04")
                {

                    flagTipoDocumento = true;

                }
                else
                {

                    Console.WriteLine("\tIngrese el Tipo de Documento correcto!!!");
                    Console.WriteLine("\t01: Documento Nacional de Identidad");
                    Console.WriteLine("\t04: Carné de Extranjería");

                }

            } while (!flagTipoDocumento);

            string numeroDocumento;
            var flagNumeroDocumento = false;

            do
            {

                Console.Write("Ingrese el Número de Documento: ");
                numeroDocumento = Console.ReadLine();

                if (
                    (
                        numeroDocumento != null
                    )
                    &&
                    (
                        (tipoDocumento == "01" && numeroDocumento.Length == 8) ||
                        (tipoDocumento == "04" && numeroDocumento.Length == 8)
                    )
                )
                {

                    flagNumeroDocumento = true;

                }
                else
                {

                    Console.WriteLine("\tIngrese el Número de Documento correcto!!!");

                }

            } while (!flagNumeroDocumento);

            Console.WriteLine();
            Console.WriteLine("Resultados:");
            Console.WriteLine();

            var colaborador = ConsultaSbs(tipoDocumento, numeroDocumento);

            if (colaborador != null)
            {

                Console.WriteLine("Apellido Paterno: {0}", colaborador.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: {0}", colaborador.ApellidoMaterno);
                Console.WriteLine("Primer Nombre: {0}", colaborador.PrimerNombre);
                Console.WriteLine("Segundo Nombre: {0}", colaborador.SegundoNombre);
                Console.WriteLine("Código de Afiliación: {0}", colaborador.CodigoAfiliacion);

            }
            else
            {

                Console.WriteLine("Documento de Identidad no registrado en el SPP.");

            }

            Console.ReadLine();

        }

    }

}
