using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace ECOMMERCE.COMPONENTS
{
    public partial class Details : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlGenericControl TtlName;
        protected System.Web.UI.HtmlControls.HtmlGenericControl LblCategory;
        protected System.Web.UI.HtmlControls.HtmlImage ImgProduct;
        protected System.Web.UI.HtmlControls.HtmlGenericControl ParContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["product"] != null)
                {
                    int productId = Convert.ToInt32(Request.QueryString["product"]);
                    LoadProductDetails(productId);
                }
                else
                {
                    // Gestire la situazione in cui l'ID del prodotto non è fornito
                    Response.Redirect("NotFound.aspx");
                }
            }
        }

        private void LoadProductDetails(int productId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MYDATABASE"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Recupera i dati dal database solo per il prodotto specificato
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Table_1 WHERE Id = @ProductId", conn);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    // Recupera i dati relativi al prodotto dal database
                    if (dataReader.HasRows)
                    {
                        // Popola l'interfaccia con i dati
                        dataReader.Read();
                        TtlName.InnerText = dataReader["MODELLO"].ToString();
                        LblCategory.InnerText = dataReader["NOME"].ToString(); // Assumi che esista una colonna "Categoria" nel tuo database
                        ImgProduct.Src = dataReader["IMMAGINE"].ToString();
                        ParContent.InnerHtml = dataReader["DESCRIZIONE"].ToString();

                    }
                    else
                    {
                        // Gestisci il caso in cui il prodotto non è trovato
                        Response.Redirect("NotFound.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Verifica se la sessione CARRELLO è già inizializzata come una lista
            if (Session["CARRELLO"] == null)
            {
                // Se non è inizializzata, crea una nuova lista
                List<string> carrello = new List<string>();
                Session["CARRELLO"] = carrello;
            }

            // Ottieni il valore dalla query string "product"
            string productId = Request.QueryString["product"];

            // Aggiungi il productId alla lista nella sessione
            List<string> carrelloAttuale = (List<string>)Session["CARRELLO"];
            carrelloAttuale.Add(productId);

            // Fai il redirect alla stessa pagina per forzare l'aggiornamento
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}