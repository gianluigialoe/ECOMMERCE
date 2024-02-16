using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ECOMMERCE.COMPONENTS
{
    public partial class HOME : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MYDATABASE"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

                // recuperare i dati dal database
                SqlCommand cmd = new SqlCommand("SELECT * FROM Table_1", conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                // inizializziamo la variabile htmlContent
                string htmlContent = "";

                if (dataReader.HasRows)
                {
                    // cicliamo sulle righe ottenute dal db a aggiungiamo l'html delle cards
                    while (dataReader.Read())
                    {
                        htmlContent += $@"<div class=""col"">
        <div class=""card h-100"">
            <img src=""{dataReader["IMMAGINE"]}"" class=""card-img-top"" alt=""{dataReader["NOME"]}"">
            <div class=""card-body d-flex flex-column"">
                <h5 class=""card-title"">{dataReader["MODELLO"]}</h5>
                <p class=""card-text"">Prezzo: {dataReader["PREZZO"]}</p>
                <a href=""Details.aspx?product={dataReader["Id"]}"" class=""btn btn-primary mt-auto"">Dettagli</a>
              
            </div>
        </div>
    </div>";
                    }
                }

                // inseriamo in RowCards il contenuto di htmlContent
                RowCards.InnerHtml = htmlContent;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
      

    
