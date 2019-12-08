using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Enfermeria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [HttpPost]
        public ContentResult AjaxMethod(string paciente)
        {
            string query = "select FE_Indicadores.indicador, FE_Indicadores.inicio, FE_Indicadores.final, COUNT(Ficha_Enfermeria.id_ficha_enf)";
            query += "from FE_Indicadores Inner join Ficha_Enfermeria On Ficha_Enfermeria.id_ficha_enf=Fe_Indicadores.id_ficha_enf Inner join Ficha on Ficha_Enfermeria.id_ficha = Ficha.id_ficha inner join Paciente on Paciente.id_paciente = Ficha.id_paciente where Paciente.id_paciente = @IdPaciente GROUP BY FE_Indicadores.indicador, FE_Indicadores.inicio, FE_Indicadores.final";
            //string query = "select K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr, COUNT(K.id_ficha_kine)";
            //query += "FROM Ficha_Kinesiologia K, Ficha F, Paciente Pa Where K.id_ficha = F.id_ficha And F.id_paciente ='" + paciente + "'group by K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr";

            string constr = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@IdPaciente", paciente);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sb.Append("[");

                        while (sdr.Read())
                        {
                            sb.Append("{");
                            System.Threading.Thread.Sleep(50);
                            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                            sb.Append(string.Format("value0:{0},value1:{1},value2:{2}", sdr[0], sdr[1], sdr[2]));
                            sb.Append("},");
                        }

                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");

                    }

                    con.Close();
                }
            }

            return Content(sb.ToString());
        }


        [HttpPost]
        public ContentResult AjaxMethod1_1(string paciente)
        {
            string query = "select Ficha_Enfermeria.fr_hta, Ficha_Enfermeria.fr_dm, Ficha_Enfermeria.fr_dlp, Ficha_Enfermeria.fr_sed, Ficha_Enfermeria.fr_spob, Ficha_Enfermeria.fr_tb, Ficha_Enfermeria.fr_oh, Ficha_Enfermeria.fr_af, Ficha_Enfermeria.fr_estres, COUNT(Ficha_Enfermeria.id_ficha_enf)";
            query += "from Ficha_Enfermeria inner join Ficha on Ficha_Enfermeria.id_ficha=Ficha.id_ficha inner join Paciente on Paciente.id_paciente = Ficha.id_paciente where Paciente.id_paciente = @IdPaciente Group by Ficha_Enfermeria.fr_hta, Ficha_Enfermeria.fr_dm, Ficha_Enfermeria.fr_dlp, Ficha_Enfermeria.fr_sed, Ficha_Enfermeria.fr_spob, Ficha_Enfermeria.fr_tb, Ficha_Enfermeria.fr_oh, Ficha_Enfermeria.fr_af, Ficha_Enfermeria.fr_estres";
            //string query = "select K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr, COUNT(K.id_ficha_kine)";
            //query += "FROM Ficha_Kinesiologia K, Ficha F, Paciente Pa Where K.id_ficha = F.id_ficha And F.id_paciente ='" + paciente + "'group by K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr";

            string constr = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@IdPaciente", paciente);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sb.Append("[");

                        while (sdr.Read())
                        {
                            sb.Append("{");
                            System.Threading.Thread.Sleep(50);
                            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                            sb.Append(string.Format("text0:'{0}',text1:'{1}',text2:'{2}',text3:'{3}',text4:'{4}',text5:'{5}',text6:'{6}',text7:'{7}',text8:'{8}'", sdr[0], sdr[1], sdr[2], sdr[3], sdr[4], sdr[5], sdr[6],sdr[7], sdr[8]));
                            sb.Append("},");
                        }

                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");

                    }

                    con.Close();
                }
            }

            return Content(sb.ToString());
        }







        [HttpPost]
        public ContentResult AjaxMethod2(string indicador)
        {
            string query = "select @Indicador, AVG(FE_Indicadores.inicio), AVG(FE_Indicadores.final)";
            query += "from FE_Indicadores where FE_Indicadores.indicador = @Indicador";
            //string query = "select K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr, COUNT(K.id_ficha_kine)";
            //query += "FROM Ficha_Kinesiologia K, Ficha F, Paciente Pa Where K.id_ficha = F.id_ficha And F.id_paciente ='" + paciente + "'group by K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr";

            string constr = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Indicador", indicador);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sb.Append("[");

                        while (sdr.Read())
                        {
                            sb.Append("{");
                            System.Threading.Thread.Sleep(50);
                            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                            sb.Append(string.Format("value0:{0},value1:{1},value2:{2}", sdr[0], sdr[1], sdr[2]));
                            sb.Append("},");
                        }

                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");

                    }

                    con.Close();
                }
            }

            return Content(sb.ToString());
        }




        [HttpPost]
        public ContentResult AjaxMethod3()
        {
            string query = "select FE_Indicadores.indicador, AVG(FE_Indicadores.inicio), AVG(FE_Indicadores.final), COUNT(id_ficha_enf)";
            query += "from FE_Indicadores GROUP by FE_Indicadores.indicador";
            //string query = "select K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr, COUNT(K.id_ficha_kine)";
            //query += "FROM Ficha_Kinesiologia K, Ficha F, Paciente Pa Where K.id_ficha = F.id_ficha And F.id_paciente ='" + paciente + "'group by K.ergo_vol_ing, K.ergo_voml_ing, K.ergo_fcmax_ing, K.ergo_vol_egr, K.ergo_voml_egr, K.ergo_fcmax_egr";

            string constr = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    //cmd.Parameters.AddWithValue("@Indicador", indicador);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sb.Append("[");

                        while (sdr.Read())
                        {
                            sb.Append("{");
                            System.Threading.Thread.Sleep(50);
                            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                            sb.Append(string.Format("value0:{0},value1:{1},value2:{2},value3:{3}", sdr[0], sdr[1], sdr[2], sdr[3]));
                            sb.Append("},");
                        }

                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");

                    }

                    con.Close();
                }
            }

            return Content(sb.ToString());
        }



    }
}