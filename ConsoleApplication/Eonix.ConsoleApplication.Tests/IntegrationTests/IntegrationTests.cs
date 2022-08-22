using Eonix.ConsoleApplication.Builders;
using Eonix.ConsoleApplication.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Eonix.ConsoleApplication.Tests.IntegrationTests;
    
[TestClass]
public class IntegrationTests
{
    [TestMethod]
    public void LesMessagesDeSortieDoiventCorrespondreAuxToursRealises()
    {
        // Given
        var output = new List<string>();
        var spectateur = SpectateurBuilder.Nouveau("Spectateur")
            .ReactionTourMusical((s, p) => output.Add($"{s.Nom} siffle au tour '{p.Tour}' de {p.Artiste}"))
            .ReactionTourAcrobatique((s, p) => output.Add($"{s.Nom} applaudit au tour '{p.Tour}' de {p.Artiste}"))
            .Creer();
        var singe1 = SingeBuilder.Nouveau("Singe 1")
            .ApprendreTourAcrobatique("Jongler")
            .ApprendreTourMusical("Taper sur un tambour")
            .Creer();
        var singe2 = SingeBuilder.Nouveau("Singe 2")
            .ApprendreTourAcrobatique("Lancer des couteaux")
            .Creer();
        var dresseur1 = new Dresseur("Dresseur 1", singe1);
        var dresseur2 = new Dresseur("Dresseur 2", singe2);

        // When
        spectateur.Observer(dresseur1.Singe);
        dresseur1.Jouer();
        spectateur.Quitter(dresseur1.Singe);
        spectateur.Observer(dresseur2.Singe);
        dresseur2.Jouer();
        spectateur.Quitter(dresseur2.Singe);

        // Then
        Assert.AreEqual(3, output.Count);
        Assert.IsTrue(output[0] == "Spectateur applaudit au tour 'Jongler' de Singe 1");
        Assert.IsTrue(output[1] == "Spectateur siffle au tour 'Taper sur un tambour' de Singe 1");
        Assert.IsTrue(output[2] == "Spectateur applaudit au tour 'Lancer des couteaux' de Singe 2");
    }
}
