using System.Windows.Input;
using DeliveryIngesup.Manager;
using DeliveryIngesup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace DeliveryIngesup.ViewModel
{
    public class InscriptionViewModel : ViewModelBase
    {
        #region Properties
        private Utilisateur _nouvelUtilisateur;

        public Utilisateur NouvelUtilisateur
        {
            get { return _nouvelUtilisateur; }
            set
            {
                _nouvelUtilisateur = value;
                RaisePropertyChanged();
            }
        }

        private string _checkPassword;

        public string CheckPassword
        {
            get { return _checkPassword; }
            set
            {
                _checkPassword = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ICommand InscriptionCommand { get; set; }
        #endregion

        public InscriptionViewModel()
        {
            NouvelUtilisateur = new Utilisateur();
            InscriptionCommand = new RelayCommand(Inscription);
        }

        private void Inscription()
        {
            if(NouvelUtilisateur.Password == CheckPassword)
                NouvelUtilisateur = UserManager.Instance.Inscription(NouvelUtilisateur);
            else
            {
                //TODO : Message d'erreur
            }
        }
    }
}