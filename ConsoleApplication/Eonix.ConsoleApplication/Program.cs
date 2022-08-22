using Eonix.ConsoleApplication.Builders;
using Eonix.ConsoleApplication.Model;

var spectateur = SpectateurBuilder.Nouveau("Spectateur")
    .ReactionTourMusical((s, p) => Console.WriteLine("{0} siffle au tour '{1}' de {2}", s.Nom, p.Tour, p.Artiste))
    .ReactionTourAcrobatique((s, p) => Console.WriteLine("{0} applaudit au tour '{1}' de {2}", s.Nom, p.Tour, p.Artiste))
    .Creer();
var singe1 = SingeBuilder.Nouveau("Singe 1")
    .ApprendreTourAcrobatique("Jongler")
    .ApprendreTourMusical("Jouer de la flûte")
    .ApprendreTourMusical("Taper sur un tambour")
    .ApprendreTourAcrobatique("Faire la roue")
    .Creer();
var singe2 = SingeBuilder.Nouveau("Singe 2")
    .ApprendreTourMusical("Fredonner la Brabançonne")
    .ApprendreTourAcrobatique("Faire un salto arrière")
    .Creer();
var dresseur1 = new Dresseur("Dresseur 1", singe1);
var dresseur2 = new Dresseur("Dresseur 2", singe2);

spectateur.Observer(dresseur1.Singe);
spectateur.Observer(dresseur2.Singe);

dresseur1.Jouer();
dresseur2.Jouer();

spectateur.Quitter(dresseur1.Singe);
spectateur.Quitter(dresseur2.Singe);
