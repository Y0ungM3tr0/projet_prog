using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class SingletonCategorie
    {
        MySqlConnection con;
        ObservableCollection<Categorie> liste_des_categories;
        static SingletonCategorie instance = null;

        public SingletonCategorie()
        {
            //420335ri_gr00001_2366599-mac-donald-etienne
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420335ri_gr00001_2366599-mac-donald-etienne;Uid=2366599;Pwd=2366599;");
            liste_des_categories = new ObservableCollection<Categorie>();
        }

        // SINGLETON
        public static SingletonCategorie getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonCategorie();
            }
            return instance;
        }

        public ObservableCollection<Categorie> getListe_des_categories()
        {
            return liste_des_categories;
        }


        // ajoute les Categories dans la liste (va les chercher dans la bd))
        public void getToutCategories()
        {
            liste_des_categories.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM categorie_activite;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idCategorie = r.GetInt32("idCategorie");
                    string type = r.GetString("type");

                    Categorie categorie = new Categorie(idCategorie, type);
                    liste_des_categories.Add(categorie);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // vérification que la connection est ouverte, pour la fermer
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // get idCategorie
        public int getIdCategories(string _type)
        {
            int idCategorie = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT idCategorie FROM categorie_activite WHERE type = @type;";

                commande.Parameters.AddWithValue("@type", _type);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    idCategorie = r.GetInt32("idCategorie");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // vérification que la connection est ouverte, pour la fermer
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return idCategorie;
        }


        // ajoute la categorie dans la bd
        public void addActivite(string _type)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO categorie_activite (type) VALUES (@type);";

                commande.Parameters.AddWithValue("@type", _type);

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                Console.WriteLine(ex.Message);
            }
            getToutCategories();
        }
    }
}
