using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECOMMERCE.COMPONENTS
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CARRELLO"] != null)
            {
                List<string> carIds = (List<string>)Session["CARRELLO"];
                decimal total = 0;

                // Connessione al database
                string connectionString = ConfigurationManager.ConnectionStrings["MYDATABASE"].ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (string carId in carIds)
                    {
                        // Esegui la query per ottenere il prezzo associato all'ID dell'auto
                        string query = "SELECT PREZZO FROM Table_1 WHERE ID = @CarId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CarId", carId);
                            object result = cmd.ExecuteScalar();

                            if (result != null && decimal.TryParse(result.ToString(), out decimal carPrice))
                            {
                                // Aggiungi il prezzo al totale
                                total += carPrice;
                            }
                        }
                    }
                }

                // Mostra il totale nel tuo controllo Label
                LblCartTotal.Text = "Totale Carrello: " + total.ToString("C"); // Formattato come valuta (es. €100.00)
            }
        }
    }
}