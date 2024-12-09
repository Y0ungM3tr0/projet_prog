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
            con = new MySqlConnection(
                SingletonUtilisateur.getInstance().getLienBd()
                );
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
        // get type de l'id
        public string getTypeCategories(int idCategorie)
        {
            string type = "";
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT type FROM categorie_activite WHERE idCategorie = @idCategorie;";

                commande.Parameters.AddWithValue("@idCategorie", idCategorie);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    type = r.GetString("type");
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

            return type;
        }


        // ajoute la categorie dans la bd
        public void addCategorie(string _type)
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

        // supprime la categorie dans la bd
        public void supprimerCategorie(int idCategorie)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Supprimer_categorie_activite(@idCategorie);";

                commande.Parameters.AddWithValue("@idCategorie", idCategorie);

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

        // modifier la categorie dans la bd
        public void modifierCategorie(int idCategorie, string type)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Modifier_categorie_activite(@idCategorie, @type);";

                commande.Parameters.AddWithValue("@idCategorie", idCategorie);
                commande.Parameters.AddWithValue("@type", type);

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
