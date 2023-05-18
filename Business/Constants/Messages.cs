using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CompanyListed = "Şirketler Listelendi.";
        public static string CommentsListed = "Yorumlar Listelendi.";
        public static string CompanyAdded = "Şirket Eklendi.";
        public static string CompanyUpdated = "Şirket Güncellendi.";
        public static string CompanyDeleted = "Şirket Silindi.";
        public static string CommentAdded = "Yorum Eklendi.";
        public static string CommentUpdated = "Yorum Güncellendi.";
        public static string CommentDeleted = "Yorum Silindi." ;
        public static string NotFoundCompany = "Listelenecek Berber Bulunamadı.";
        public static string NotFoundComment = "Yorum Bulunamadı.";
        public static string UserRegistered = "Kullanıcı kaydoldu.";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifreniz yanlış";
        public static string SuccessfulLogin= "Giriş Başarılı";
        public static string UserAlreadyExists = "Kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu.";

        public static string PasswordUpdated = "Parola Başarıyla güncellendi.";

        public static string NewPasswordsMatchError = "Yeni parolalarınız eşleşmiyor.";
        public static string PasswordsSame = "Eski ve Yeni şifre aynı";

        public static string AuthorizationDenied = "Yetkiniz Yok";
    }
}
