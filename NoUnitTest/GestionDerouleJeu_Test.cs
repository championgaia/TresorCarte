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
        private IMapManage_BLL _oIGestionCarte_BLL { get; set; }
        private IGameManage_BLL _oIGestionDerouleJeu_BLL { get; set; }
        private IAdventurerManage_BLL _oIGestionJoueur_BLL { get; set; }
        [SetUp]
        public void Setup()
        {
            _oIGestionCarte_BLL = new MapManage_BLL(new DataValideManage_BLL());
            _oIGestionJoueur_BLL = new AdventurerManage_BLL(new DataValideManage_BLL());
            _oIGestionDerouleJeu_BLL = new GameManage_BLL(_oIGestionCarte_BLL, _oIGestionJoueur_BLL, new DeplacementManage_BLL(), new FileManage_BLL());
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
            Assert.IsNull(await _oIGestionDerouleJeu_BLL.RunGameAsync(null));
            //Test sioListDescriptionFichier est une collection vide
            Assert.IsNull(await _oIGestionDerouleJeu_BLL.RunGameAsync(new List<string>()));
            //Test si oCurrentJeu.NombreDeplacementMax = 0
            Game oCurrentJeu = new Game
            {
                Map = await _oIGestionCarte_BLL.CreateTreasureMapAsync(Lists_Donnes_Valide),
                Players = await _oIGestionJoueur_BLL.CreateListeAdventurerAsync(Lists_Donnes_Valide)
            };
            oCurrentJeu.MaxTurn = 0;
            await _oIGestionDerouleJeu_BLL.SetGameAsync(oCurrentJeu);
            Assert.IsNull(await _oIGestionDerouleJeu_BLL.RunGameAsync(null));
            await _oIGestionDerouleJeu_BLL.SetGameAsync(null);
        }
    }
}
