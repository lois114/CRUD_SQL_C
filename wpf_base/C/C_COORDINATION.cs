using NS_WS;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace wpf_base.C
{

    class C_COORDINATION : C_NOTIFIABLE
    {
        WEB_API Le_WS;

        public C_COORDINATION()
        {
            try {
                Le_WS = new WEB_API("https://serv-lol.azurewebsites.net/", new HttpClient());
                Les_Roles = Le_WS.GetAllRoleAsync().Result.ToList();
            }
            catch (Exception ex) {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
            

        }

        private List<C_PERSONNAGES> _Les_Personnages;

        public List<C_PERSONNAGES> Les_Personnages {
            get { return _Les_Personnages; }
            set {
                _Les_Personnages = value;
                Signale_Changement();
            }
        }

        private List<C_ROLES> _Les_Roles;

        public List<C_ROLES> Les_Roles {
            get { return _Les_Roles; }
            set {
                _Les_Roles = value;
               
                Signale_Changement();
            }
        }

        private C_ROLES _Role_Select;

        public C_ROLES Role_Select {
            get { return _Role_Select; }
            set {
                _Role_Select = value;
                Peut_Sup_Role = value != null;
                Signale_Changement();
            }
        }

        private C_PERSONNAGES _Perso_Select;

        public C_PERSONNAGES Perso_Select {
            get { return _Perso_Select; }
            set {
                _Perso_Select = value;
                Peut_Sup_Personne = value != null;

                Signale_Changement();
            }
        }


        private C_PERSONNAGES _Copie_Personnage;

        public C_PERSONNAGES Copie_Personnage {
            get { return _Copie_Personnage; }
            set { _Copie_Personnage = value;
                Peut_Modif_Perso = value != null; 
                Signale_Changement(); }
        }


        // GESTION ADD ROLE 

        private string _Nom_Nouveau_Role;

        public string Nom_Nouveau_Role {
            get { return _Nom_Nouveau_Role; }
            set { _Nom_Nouveau_Role = value; Signale_Changement(); }
        }

        internal void Add_Roles()
        {

            try {

                var Nouveau_Role = new C_ROLES() { Nom_Role = Nom_Nouveau_Role };

                Le_WS.AddRolesAsync(Nouveau_Role.Nom_Role);

                Mise_a_jour_liste_role();

            }
            catch (Exception ex) {

                MessageBox.Show($"Erreur : {ex.Message}");

            }


        }

        internal void Mise_a_jour_liste_role()
        {
            try {
                Les_Roles = Le_WS.GetAllRoleAsync().Result.ToList();
            }
            catch (Exception ex) {

                MessageBox.Show($"Erreur : {ex.Message}");

            }
        }


        // GESTION ADD PERSO 


        private bool _Peut_Add_Personne;

        public bool Peut_Add_Personne {
            get { return _Peut_Add_Personne; }
            set {
                _Peut_Add_Personne = value;
                Signale_Changement();
            }
        }



        private string _Nom_Nouveau_Personnage;

        public string Nom_Nouveau_Personnage {
            get { return _Nom_Nouveau_Personnage; }
            set {
                _Nom_Nouveau_Personnage = value.ToUpper();

                Signale_Changement();
            }
        }


        private string _Url_Image;

        public string Url_Image {
            get { return _Url_Image; }
            set { _Url_Image = value;
                Signale_Changement();
            }
        }

       
        internal void Affiche_Personnage()
        {
            if (Role_Select != null) {

                Les_Personnages = Le_WS.GetPersobyRoleAsync(Role_Select.Id_Roles).Result.ToList();

                Peut_Add_Personne = true;


            }
            
            
          
        }

        internal void Add_Personnage()
        {
            if (Role_Select != null) {

               


                if (Nom_Nouveau_Personnage != "" && Url_Image != "") {
                    try {
                        var Nouveau_Pers = new C_PERSONNAGES() { Nom_Personnage = Nom_Nouveau_Personnage, Id_Roles = Role_Select.Id_Roles, Image = Url_Image };

                        Le_WS.AddPersoAsync(Nouveau_Pers);

                        Affiche_Personnage();

                        if (Uri.IsWellFormedUriString(Url_Image, UriKind.Absolute)) {



                        } else {
                            MessageBox.Show("Vous avez rentrer un URL invalide");

                        }


                        Nom_Nouveau_Personnage = "";
                        Url_Image = "";
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Erreur lors de l'ajout du personnage : {ex.Message}");
                    }
                }else {
                    MessageBox.Show("Veuillez remplir tous les champs nécessaires.");

                }

               






            }


        }


        //------------------------------------------------------------------------------------------------------------------------



        internal void Debute_La_Modif_Personne()
        {
            if (Perso_Select != null) {
                Copie_Personnage = new C_PERSONNAGES() {
                    Id_Personnages = Perso_Select.Id_Personnages,
                    Nom_Personnage = Perso_Select.Nom_Personnage,
                    Id_Roles = Perso_Select.Id_Roles,
                    Image = Perso_Select.Image,
                };

            }
        }


        internal void Effectuer_La_Modif_Personne()
        {
            if (Copie_Personnage != null) {

                Le_WS.ModifPersoAsync(Copie_Personnage);
                Copie_Personnage = null;

                Affiche_Personnage();
            }
        }


        private bool _Peut_Modif_Perso;

        public bool Peut_Modif_Perso {
            get { return _Peut_Modif_Perso; }
            set {
                _Peut_Modif_Perso = value;
                Signale_Changement();
            }
        }


        // SUPRESSION // 


        // SUP PERSO 


        private bool _Peut_Sup_Personne;

        public bool Peut_Sup_Personne
 {
            get { return _Peut_Sup_Personne; }
            set { _Peut_Sup_Personne = value;
                Signale_Changement();
            }
        }

        public void Sup_Perso()
        {
            if (Perso_Select != null) {

                Le_WS.DeletePersonnagesAsync(Perso_Select.Id_Personnages);

                Affiche_Personnage();



            }

            
            }


        // SUP ROLE


        private bool _Peut_Sup_Role;

        public bool Peut_Sup_Role {
            get { return _Peut_Sup_Role; }
            set {
                _Peut_Sup_Role = value;
                Signale_Changement();
            }
        }

        internal void Sup_Role()
        {
            if (Role_Select != null) {

                Le_WS.DeleteRolesAsync(Role_Select.Id_Roles);

                Mise_a_jour_liste_role();
              


            }
        }
    }
}
