using NS_WS;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpf_base.C;



namespace wpf_base
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        C_COORDINATION La_Coordination;
        public MainWindow()
        {
            InitializeComponent();
            La_Coordination = new C_COORDINATION();
            DataContext = La_Coordination;
        }

        private void BTN_ADD_ROLES_Click(object sender, RoutedEventArgs e)
        {
            La_Coordination.Add_Roles();
        }

        private void BTN_ADD_PERSO_Click(object sender, RoutedEventArgs e)
        {
            La_Coordination.Add_Personnage();
            La_Coordination.Affiche_Personnage();

            
            
        }

        private void LST_ROLES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            La_Coordination.Affiche_Personnage();
           

        }

        private void Affiche_Image(string P_URL)
        {

            try {
                BitmapImage image = new BitmapImage();


                image.BeginInit();
                image.UriSource = new Uri(P_URL, UriKind.Absolute);
                image.EndInit();


                IMAGE_LOL.Source = image;
            }
            catch  (Exception ex) {
                MessageBox.Show($"Votre Url est incorrect : {ex.Message}");
                IMAGE_LOL.Source = new BitmapImage(new Uri("https://yt3.googleusercontent.com/V3r1ulI_qpPjFwr0Nq5y2d0LewJaGlyJyrfK9u9jGEuBQfCQ2vbh6Pc5DcXHkucyi9FCYGSG=s900-c-k-c0x00ffffff-no-rj"));

            }



        }

        private void LST_PERSONNAGES_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LST_PERSONNAGES.Items.Count != 0) {

                var Personnage = (C_PERSONNAGES)LST_PERSONNAGES.SelectedItem;
                if (Personnage != null) {
                    if (Personnage.Image != null) {
                        Affiche_Image(Personnage.Image);
                    } else { Affiche_Image("https://yt3.googleusercontent.com/V3r1ulI_qpPjFwr0Nq5y2d0LewJaGlyJyrfK9u9jGEuBQfCQ2vbh6Pc5DcXHkucyi9FCYGSG=s900-c-k-c0x00ffffff-no-rj"); }

                }

            }
           
           
        }

       

        private void SUP_PERSO_Click_1(object sender, RoutedEventArgs e)
        {
            La_Coordination.Sup_Perso();
        }

        private void SUP_ROLE_Click(object sender, RoutedEventArgs e)
        {
            La_Coordination.Sup_Role();
        }
        private void LST_PERSONNAGES_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            La_Coordination.Debute_La_Modif_Personne();
        }
        private void BTN_MODIF_PERSO_Click(object sender, RoutedEventArgs e)
        {
            La_Coordination.Effectuer_La_Modif_Personne();
        }

        
    }
}