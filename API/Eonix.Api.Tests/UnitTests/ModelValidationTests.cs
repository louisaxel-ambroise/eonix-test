using Eonix.Api.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eonix.Api.Tests;

[TestClass]
public class ModelValidationTests
{
    [TestMethod]
    [DataRow("Valide", "Valide", true)]
    [DataRow("CeNomEstTellementLongQuIlDepasseLeNombreDeCaractereAutoriseDansLaBaseDeDonneesMaisEgalementCeluiConfigureDansLesAttributsDeValidationDuModele", "Valide", false)]
    [DataRow("Valide", "CePreomEstTellementLongQuIlDepasseLeNombreDeCaractereAutoriseDansLaBaseDeDonneesMaisEgalementCeluiConfigureDansLesAttributsDeValidationDuModele", false)]
    [DataRow("", "Valide", false)]
    [DataRow(null, "Valide", false)]
    [DataRow("Valide", "", false)]
    [DataRow("Valide", null, false)]
    [DataRow("   ", null, false)]
    [DataRow("   ", "", false)]
    public void CreatePersonneRequestValidationTests(string nom, string prenom, bool shouldBeValid)
    {
        // Given
        var request = new CreatePersonneRequest { Nom = nom, Prenom = prenom };
        var context = new ValidationContext(request, null, null);
        var results = new List<ValidationResult>();

        // When
        var isModelStateValid = Validator.TryValidateObject(request, context, results, true);

        // Then
        Assert.AreEqual(shouldBeValid, isModelStateValid);
    }

    [TestMethod]
    [DataRow("Valide", "Valide", true)]
    [DataRow("CeNomEstTellementLongQuIlDepasseLeNombreDeCaractereAutoriseDansLaBaseDeDonneesMaisEgalementCeluiConfigureDansLesAttributsDeValidationDuModele", "Valide", false)]
    [DataRow("Valide", "CePreomEstTellementLongQuIlDepasseLeNombreDeCaractereAutoriseDansLaBaseDeDonneesMaisEgalementCeluiConfigureDansLesAttributsDeValidationDuModele", false)]
    [DataRow("", "Valide", false)]
    [DataRow(null, "Valide", false)]
    [DataRow("Valide", "", false)]
    [DataRow("Valide", null, false)]
    [DataRow("   ", null, false)]
    [DataRow("   ", "", false)]
    public void UpdatePersonneRequestValidationTests(string nom, string prenom, bool shouldBeValid)
    {
        // Given
        var request = new UpdatePersonneRequest { Nom = nom, Prenom = prenom };
        var context = new ValidationContext(request, null, null);
        var results = new List<ValidationResult>();

        // When
        var isModelStateValid = Validator.TryValidateObject(request, context, results, true);

        // Then
        Assert.AreEqual(shouldBeValid, isModelStateValid);
    }
}
