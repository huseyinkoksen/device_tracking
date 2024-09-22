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
        public static string DeviceListed = "Cihazler Listelendi.";
        public static string EmployeesListed = "Çalışanlar Listelendi.";
        public static string DeviceAdded = "Cihaz Eklendi.";
        public static string DeviceUpdated = "Cihaz Güncellendi.";
        public static string DeviceDeleted = "Cihaz Silindi.";
        public static string EmployeeAdded = "Çalışan Eklendi.";
        public static string EmployeeUpdated = "Çalışan Güncellendi.";
        public static string EmployeeDeleted = "Çalışan Silindi." ;
        public static string NotFoundDevice = "Listelenecek Berber Bulunamadı.";
        public static string NotFoundEmployee = "Çalışan Bulunamadı.";
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
        public static string EmployeeNotExist = "Çalışan bulunamadı.";
        public static string EmployeeExist = "Çalışan mevcut.";
    }
}
