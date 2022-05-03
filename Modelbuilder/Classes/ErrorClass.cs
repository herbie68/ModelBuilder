using Google.Protobuf.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Modelbuilder.HelperGeneral;

namespace Modelbuilder;

/// <summary>
/// Used Error codes
///  1 => Product already exists
///  2 => Worktype already exists
///  3 => Brand already excists
///  4 => Unit Already Excists
///  5 => Category already excists
///  6 => Storage already exists
///  7 => Project already exists
///  8 => Contacttype already excists
///  9 => Supplier already excists
/// 10 => Currency already excists
/// 11 => Country already excists
/// 21 => Not existing ProductCode
/// 22 => Not existing WorktypeName
/// 23 => Not existing BrandId
/// 24 => Not existing UnitId
/// 25 => Not existing Category
/// 26 => Not existing StorageId
/// 27 => Not existing ProjectId
/// 28 => Not existing Contacttype
/// 29 => Not existing Supplier
/// 30 => Not existing CurrencyId
/// 31 => Not existing CountryId
/// 40 => Endtime bigger or equal then Starttime
/// 41 => Incorrect number of fields in CSV file
/// </summary>
internal class ErrorClass
{
    public ErrorClass()
    {
        // Nothing to do her yet
    }

    public string GetSingleErrorMessage(int Error)
    {
        // Use default error in case no matching error is found
        var ErrorMessage = Languages.Cultures.Import_Messagebox_Error_Default_Long;

        switch (Error)
        {
            case 40:
                // Endtime bigger or equal then Starttime
                ErrorMessage = Languages.Cultures.Import_Messagebox_Error_StartEndTimeError;
                break;
            case 41:
                // Incorrect number of fields in CSV file
                ErrorMessage = Languages.Cultures.Import_Messagebox_Error_HeaderError;
                break;
        }
        return ErrorMessage;
    }
    public (string Label, string ErrorMessageShort, string ErrorMessageLong) GetErrorMessages(int Error)
    {
        // Use default error in case no matching error is found
        var Label=Languages.Cultures.Import_Messagebox_Error_Default_Label;
        var ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_Default;
        var ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_Default_Long;
        switch (Error)
        {
            case 1:
                // Product already exists
                Label = Languages.Cultures.ImportProducts_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_ProductAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_ProductAlreadyExists_Long;
                break;
            case 2:
                // Worktype already exists
                Label = Languages.Cultures.ImportWorktypes_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_WorktypeAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_WorktypeAlreadyExists_Long;
                break;
            case 3:
                // Brand already excists
                Label = Languages.Cultures.ImportBrands_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_BrandAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_BrandAlreadyExists_Long;
                break;
            case 4:
                // Unit Already Excists
                Label= Languages.Cultures.ImportUnits_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_UnitAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_UnitAlreadyExists_Long;
                break;
            case 5:
                // Category already excists
                Label = Languages.Cultures.ImportCategories_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_CategoryAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_CategoryAlreadyExists_Long;
                break;
            case 6:
                // Storage already exists
                Label = Languages.Cultures.ImportStorages_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_StorageAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_StorageAlreadyExists_Long;
                break;
            case 7:
                // Project already exists
                Label= Languages.Cultures.ImportProjects_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_ProjectAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_ProjectAlreadyExists_Long;
                break;
            case 8:
                // Contacttype already excists
                Label = Languages.Cultures.ImportContactTypes_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_ContacttypeAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_ContacttypeAlreadyExists_Long;
                break;
            case 9:
                // Supplier already excists
                Label = Languages.Cultures.ImportSuppliers_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_SupplierAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_SupplierAlreadyExists_Long;
                break;
            case 10:
                // Currency already excists
                Label = Languages.Cultures.ImportCurrencies_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_CurrencyAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_CurrencyAlreadyExists_Long;
                break;
            case 11:
                // Country already excists
                Label = Languages.Cultures.ImportCountries_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_CountryAlreadyExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_CountryAlreadyExists_Long;
                break;
            case 21:
                // Not existing ProductCode
                Label = Languages.Cultures.ImportProducts_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_ProductNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_ProductNotExists_Long;
                break;
            case 22:
                // Not existing WorktypeName
                Label = Languages.Cultures.ImportWorktypes_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_WorktypeNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_WorktypeNotExists_Long;
                break;
            case 23:
                // Not existing BrandId
                Label = Languages.Cultures.ImportBrands_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_BrandNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_BrandNotExists_Long;
                break;
            case 24:
                // Not existing UnitId
                Label = Languages.Cultures.ImportUnits_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_UnitNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_UnitNotExists_Long;
                break;
            case 25:
                // Not existing Category
                Label = Languages.Cultures.ImportCategories_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_CategoryNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_CategoryNotExists_Long;
                break;
            case 26:
                // Not existing StorageId
                Label = Languages.Cultures.ImportStorages_Label; 
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_StorageNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_StorageNotExists_Long;
                break;
            case 27:
                // Not existing ProjectId
                Label = Languages.Cultures.ImportProjects_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_ProjectNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_ProjectNotExists_Long;
                break;
            case 28:
                // Not existing Contacttype
                Label = Languages.Cultures.ImportContactTypes_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_ContacttypeNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_ContacttypeNotExists_Long;
                break;
            case 29:
                // Not existing Supplier
                Label = Languages.Cultures.ImportSuppliers_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_SupplierNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_SupplierNotExists_Long;
                break;
            case 30:
                // Not existing Currency
                Label = Languages.Cultures.ImportCurrencies_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_CurrencyNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_CurrencyNotExists_Long;
                break;
            case 31:
                // Not existing Country
                Label = Languages.Cultures.ImportCountries_Label;
                ErrorMessageShort = Languages.Cultures.Import_Messagebox_Error_CountryNotExists;
                ErrorMessageLong = Languages.Cultures.Import_Messagebox_Error_CountryNotExists_Long;
                break;
        }
        return (Label, ErrorMessageShort, ErrorMessageLong);
    }
}