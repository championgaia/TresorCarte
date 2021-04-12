using BLL;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoUnitTest
{
    public class GestionDerouleJeu_Test : NoUnitTestBase
    {
        private IGestionCarte_BLL _oIGestionCarte_BLL { get; set; }
        private IGestionDerouleJeu_BLL _oIGestionDerouleJeu_BLL { get; set; }
        private IGestionJoueur_BLL _oIGestionJoueur_BLL { get; set; }
        [SetUp]
        public void Setup()
        {
            _oIGestionCarte_BLL = new GestionCarte_BLL(new GestionDonnesValide_BLL());
            _oIGestionJoueur_BLL = new GestionJoueur_BLL(new GestionDonnesValide_BLL());
            _oIGestionDerouleJeu_BLL = new GestionDerouleJeu_BLL(_oIGestionCarte_BLL, _oIGestionJoueur_BLL, new DeplacerJoueur_BLL(), new GestionFichier_BLL());
        }
        /// <summary>
        /// test ExecuteJeu retouner null
        /// Task<string> ExecuterJeu(List<string> oListDescriptionFichier)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ExecuterJeu_TestNull()
        {
            //Test si oListDescriptionFichier = null;
            Assert.IsNull(await _oIGestionDerouleJeu_BLL.ExecuterJeu(null));
            //Test sioListDescriptionFichier est une collection vide
            Assert.IsNull(await _oIGestionDerouleJeu_BLL.ExecuterJeu(new List<string>()));
            //Test si oCurrentJeu.NombreDeplacementMax = 0
            Jeu oCurrentJeu = new Jeu
            {
                Carte = await _oIGestionCarte_BLL.CreateCarteTresor(Lists_Donnes_Valide),
                Joueurs = await _oIGestionJoueur_BLL.CreateListeJoueur(Lists_Donnes_Valide)
            };
            oCurrentJeu.NombreDeplacementMax = 0;
            await _oIGestionDerouleJeu_BLL.SetJeu(oCurrentJeu);
            Assert.IsNull(await _oIGestionDerouleJeu_BLL.ExecuterJeu(null));
            await _oIGestionDerouleJeu_BLL.SetJeu(null);
        }
    }
}
