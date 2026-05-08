using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Resources
{
     public static class SharedResourcesKeys
    {
        public const string Required = "Required";
        public const string NotFound = "NotFound";
        public const string Deleted = "Deleted";
        public const string Created = "Created";
        public const string Success = "Success";
        public const string NotEmpty = "NotEmpty";
        public const string Updated = "Updated";
        public const string UnAuthorized = "UnAuthorized";
        public const string BadRequest = "BadRequest";
        public const string UnprocessableEntity = "UnprocessableEntity";
        public const string MaxLengthis100 = "MaxLengthis100";
        public const string MaxLengthis1000 = "MaxLengthis1000";
        public const string MaxLengthis2000 = "MaxLengthis2000";

        public const string IsExist = "IsExist";
        public const string IsNotExist = "IsNotExist";
        public const string DepartmementId = "DepartmementId";
        public const string PasswordNotEqualConfirmPass = "PasswordNotEqualConfirmPass";
        public const string EmailOrPhoneIsExist = "EmailOrPhoneIsExist";
        public const string UserNameIsExist = "UserNameIsExist";
        public const string FaildToAddUser = "FaildToAddUser";
        public const string UpdateFailed = "UpdateFailed";
        public const string DeletedFailed = "DeletedFailed";
        public const string ChangePassFailed = "ChangePassFailed";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string UserNameIsNotExist = "UserNameIsNotExist";
        public const string PasswordNotCorrect = "PasswordNotCorrect";

        public const string AlgorithmIsWrong = "AlgorithmIsWrong";
        public const string TokenIsNotExpired = "TokenIsNotExpired";
        public const string RefreshTokenIsNotFound = "RefreshTokenIsNotFound";
        public const string RefreshTokenIsExpired = "RefreshTokenIsExpired";
        public const string TokenIsExpired = "TokenIsExpired";
        public const string AddFailed = "AddFailed";
        public const string RoleNotExist = "RoleNotExist";
        public const string RoleIsUsed = "RoleIsUsed";
        public const string UserIsNotFound = "UserIsNotFound";
        public const string FailedToRemoveOldRoles = "FailedToRemoveOldRoles";
        public const string FailedToUpdateUserRoles = "FailedToUpdateUserRoles";
        public const string FailedToAddNewRoles = "FailedToAddNewRoles";

        public const string FailedToUpdateClaims = "FailedToUpdateClaims";
        public const string FailedToAddNewClaims = "FailedToAddNewClaims";
        public const string FailedToRemoveOldClaims = "FailedToRemoveOldClaims";
        public const string Email = "Email";
        public const string Message = "Message";
        public const string SendEmailFailed = "SendEmailFailed";
        public const string EmailOrPhoneNotConfirmed = "EmailOrPhoneNotConfirmed";
        public const string TryToRegisterAgain = "TryToRegisterAgain";
        public const string ErrorWhenConfirmEmail = "ErrorWhenConfirmEmail";
        public const string ConfirmEmailDone = "ConfirmEmailDone";
        public const string TryAgainInAnotherTime = "TryAgainInAnotherTime";
        public const string InvaildCode = "InvaildCode";
        public const string NoImage = "NoImage";
        public const string FailedToUploadImage = "FailedToUploadImage";
        public const string Add_Correct_info = "Add_Correct_info";
        public const string PhoneNumberNotConfirmed = "PhoneNumberNotConfirmed";
        public const string ConfirmPhoneDone = "ConfirmPhoneDone";
        public const string OTP_IsWrong = "OTP_IsWrong";
        public const string CodeIsWrong = "CodeIsWrong";

        public const string MaxLengthis255 = "MaxLengthis255";
        public const string PriceGreaterThanZero = "PriceGreaterThanZero";
        public const string StockGreaterThanZero = "StockGreaterThanZero";

        public const string DiscountBetween0And100 = "DiscountBetween0And100";
        public const string AddCorrectValue = "AddCorrectValue";

        public const string VariantMetaDataRequired = "VariantMetaDataRequired";
        public const string VariantColorsRequired = "VariantColorsRequired";
        public const string ImageUrlCannotBeEmpty = "ImageUrlCannotBeEmpty";

        public const string YouMustCreateStoreFirst = "YouMustCreateStoreFirst";
        public const string CategoryNotFound = "CategoryNotFound"; 
        public const string FailedToAddProduct = "FailedToAddProduct";
        public const string ProductAddedSuccessfully = "ProductAddedSuccessfully";
        public const string ProductNotFound = "ProductNotFound";
        public const string ProductUpdatedSuccessfully = "ProductUpdatedSuccessfully";

        public const string ProductDeleteFailed = "ProductDeleteFailed";
        public const string ProductRestoredSuccessfully = "ProductRestoredSuccessfully";
        public const string ProductIsAlreadyActive = "ProductIsAlreadyActive";

        public const string ErrorWhileAddingProduct = "ErrorWhileAddingProduct";
        public const string YouCannotUpdateThisProduct = "YouCannotUpdateThisProduct";
        public const string YouCannotDeleteThisProduct = "YouCannotDeleteThisProduct";

        public const string ProductDeletedSuccessfully = "ProductDeletedSuccessfully";
        public const string EmailandPhoneAreNotFound = "EmailandPhoneAreNotFound";
        public const string CancelPaymentOperation = "CancelPaymentOperation";
        public const string Google_email_not_verified = "Google_email_not_verified";

        public const string SellerNotFound = "SellerNotFound";
        public const string FailedToAddStore = "FailedToAddStore";
        public const string StoreAddedSuccessfully = "StoreAddedSuccessfully";
        public const string SellerHasStore = "SellerHasStore";
        public const string NotOwner = "NotOwner";
        public const string StoreNotFound = "StoreNotFound";
        public const string StoreUpdatedSuccessfully = "StoreUpdatedSuccessfully";
        public const string StoreAlreadyActivated = "StoreAlreadyActivated";
        public const string StoreActivatedSuccessfully = "StoreActivatedSuccessfully";
        public const string InvalidStoreId = "InvalidStoreId";
        public const string StoreIsDeleted = "StoreIsDeleted";
        public const string StoreAlreadyNotActive = "StoreAlreadyNotActive";
        public const string StoreDeactivateSuccessfully = "StoreDeactivateSuccessfully";
        public const string StoreAlreadyDeleted = "StoreAlreadyDeleted";
        public const string StoreRestoredSuccessfully = "StoreRestoredSuccessfully";
        public const string StoreIsNotDeleted = "StoreIsNotDeleted";


        public const string ProductVariantNotFound = "ProductVariantNotFound";
        public const string DiscountDeletedSuccessfully = "DiscountDeletedSuccessfully";
        public const string DiscountAddedSuccessfully = "DiscountAddedSuccessfully";
        public const string ErrorWhenTryCreateUserByGoogle = "ErrorWhenTryCreateUserByGoogle";
        public const string OrderItemsNotFound = "OrderItemsNotFound";



        public const string InvalidRatingValue = "InvalidRatingValue";
        public const string CommentTooLong = "CommentTooLong";
        public const string YouCannotReviewYourOwnProduct = "YouCannotReviewYourOwnProduct";
        public const string YouAlreadyReviewedThisProduct = "YouAlreadyReviewedThisProduct";
        public const string ReviewAddedSuccessfully = "ReviewAddedSuccessfully";
        public const string ReviewNotFound = "ReviewNotFound";
        public const string YouCanOnlyEditYourReview = "YouCanOnlyEditYourReview";
        public const string ReviewUpdatedSuccessfully = "ReviewUpdatedSuccessfully";
        public const string ReviewDeletedSuccessfully = "ReviewDeletedSuccessfully";


        public const string MaxSizeIs5MB = "MaxSizeIs5MB";
        public const string OnlyImagesAllowed = "OnlyImagesAllowed";
        public const string MustBeGreaterThanZero = "MustBeGreaterThanZero";
        public const string ImageAddedSuccessfully = "ImageAddedSuccessfully";
        public const string ImageUpdatedSuccessfully = "ImageUpdatedSuccessfully";
        public const string ImageDeletedSuccessfully = "ImageDeletedSuccessfully";
        public const string ImageNotFound = "ImageNotFound";


    
        public const string ProductIdGreaterThanZero = "ProductIdGreaterThanZero";
        public const string VariantAlreadyExists = "VariantAlreadyExists";
        public const string VariantupdatedSuccessfully = "VariantupdatedSuccessfully";
        public const string VariantNotFound = "VariantNotFound";
        public const string ProductVariantAlreadyDeleted = "ProductVariantAlreadyDeleted";
        public const string VariantDeletedSuccessfully = "VariantDeletedSuccessfully";
        public const string CannotRestoreVariantProductDeleted = "CannotRestoreVariantProductDeleted";
        public const string VariantIsNotDeleted = "VariantIsNotDeleted";
        public const string VariantRestoredSuccessfully = "VariantRestoredSuccessfully";

        
        public const string AlreadyAddedToFavorites = "AlreadyAddedToFavorites";
        public const string AddedToFavoritesSuccessfully = "AddedToFavoritesSuccessfully";
        public const string FavoriteNotFound = "FavoriteNotFound";
        public const string RemovedFromFavoritesSuccessfully = "RemovedFromFavoritesSuccessfully";



        public const string InvalidShipmentStatus = "InvalidShipmentStatus";
        public const string OrderNotFound = "OrderNotFound";
        public const string OrderNotPaid = "OrderNotPaid";
        public const string AlreadyShipped = "AlreadyShipped";
        public const string ShipmentCreatedSuccessfully = "ShipmentCreatedSuccessfully";
        public const string ShipmenttrackingNotFound = "ShipmenttrackingNotFound";


        public const string ShipmentNotFound = "ShipmentNotFound";
        public const string StatusAlreadySet = "StatusAlreadySet";
        public const string CannotUpdateFinalStatus = "CannotUpdateFinalStatus";
        public const string ShipmentStatusUpdatedSuccessfully = "ShipmentStatusUpdatedSuccessfully";



        public const string CategoryRestoredSuccessfully = "CategoryRestoredSuccessfully";
        public const string CategoryIsNotDeleted = "CategoryIsNotDeleted";
        public const string CategoryDeletedSuccessfully = "CategoryDeletedSuccessfully";
        public const string CategoryHasProducts = "CategoryHasProducts";
        public const string CategoryHasChildren = "CategoryHasChildren";
        public const string CategoryAlreadyDeleted = "CategoryAlreadyDeleted";
        public const string CategoryUpdatedSuccessfully = "CategoryUpdatedSuccessfully";
        public const string CategoryNameAlreadyExists = "CategoryNameAlreadyExists";
        public const string InvalidParentCircularReference = "InvalidParentCircularReference";
        public const string ParentCategoryNotFound = "ParentCategoryNotFound";
        public const string CategoryAddSuccessfully = "CategoryAddSuccessfully";
        public const string ParentCategoryIdMustGreaterThan0 = "ParentCategoryIdMustGreaterThan0";
        public const string CategoryCannotBeParentOfItself = "CategoryCannotBeParentOfItself";
        public const string OrderDeletedSuccessfully = "OrderDeletedSuccessfully";
        public const string OrderAlreadyShipped = "OrderAlreadyShipped";
        public const string CannotDeletePaidOrder = "CannotDeletePaidOrder";


        public const string IdMustBeGreaterThanZero = "IdMustBeGreaterThanZero";

        public const string MaxSizeIs100MB = "MaxSizeIs100MB";

        public const string ContentTooLong = "ContentTooLong";
        public const string Allowedextensions = "Allowedextensions";
        public const string MaxNumberOfFilles4 = "MaxNumberOfFilles4";



    }
}
