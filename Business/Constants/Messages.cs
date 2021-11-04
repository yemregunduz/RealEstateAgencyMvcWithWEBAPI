using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        internal static string UserImagePathUpdated = "Kullanıcının fotoğrafı güncellendi.";
        internal static string UserImageAdded = "Fotoğraf kullanıcıya eklendi.";
        internal static string ImageNotFound = "Fotoğraf bulunamadı.";
        internal static string UserImageDeleted = "Kullanıcın fotoğrafı silindi.";
        internal static string UserImageLimitExceeded = "Bir kullanıcıya ait fotoğraf sayısı 5'i geçemez.";
        internal static string UserImageUpdated = "Kullanıcının fotoğrafı güncellendi.";
        internal static string ProductImagePathIsRequired = "Resim linki alınamadı.";
        internal static string UserImagePathIsRequired = "Resim linki alınamadı.";
        internal static string AuthorizationDenied = "Yetkiniz yok.";
        internal static string RealEstateImageAdded = "Fotoğraf gayrımenkule eklendi.";
        internal static string RealEstateIdIsRequired = "Gayrımenkul belirtilmelidir.";
        internal static string CityAlreadyExist = "Şehir daha önce eklenmiş.";
        internal static string UserIdIsRequired = "Kullanıcı girmek zorunludur.";
        internal static string CityAdded = "Şehir eklendi.";
        internal static string CityDeleted = "Şehir silindi.";
        internal static string CitiesListed = "Şehirler listelendi.";
        internal static string CityUpdated = "Şehir güncellendi.";
        internal static string DistrictAlreadyExist = "İlçe daha önce eklenmiş.";
        internal static string DistrictAdded = "İlçe eklendi.";
        internal static string DistrictDeleted = "İlçe silindi";
        internal static string DistrictsListed = "İlçeler listelendi.";
        internal static string DistrictUpdated = "İlçe güncellendi.";
        internal static string DistrictListedByCityId = "İlçeler şehre göre listelendi.";
        internal static string CityNameIsNotValid = "Şehir adı maksimum 50 karakter olmalıdır.";
        internal static string CityNameIsRequired = "Şehir adı girmek zorunludur.";
        internal static string DistrictNameIsNotValid = "İlçe adı maksimum 100 karakter olmalıdır.";
        internal static string DistrictNameIsRequired = "İlçe adı girmek zorunludur.";
        internal static string NeighborhoodAlreadyExist = "Mahalle daha önce eklenmiş.";
        internal static string NeighborhoodAdded = "Mahalle eklendi.";
        internal static string NeighborhoodDeleted = "Mahalle silindi.";
        internal static string NeighborhoodListed = "Mahalleler listelendi.";
        internal static string NeighborhoodListedByDistrictId = "Mahalleler ilçeye göre listelendi.";
        internal static string NeighborhoodNameIsRequired = "Mahalle adı girmek zorunludur.";
        internal static string NeighborhoodIsNotValid = "Mahalle adı maksimum 100 karakter olmalıdır.";
        internal static string NumberOfRoomAdded = "Oda sayısı seçeneği eklendi.";
        internal static string NumberOfRoomDeleted = "Oda sayısı seçeneği silindi.";
        internal static string NumberOfRoomListed = "Oda sayısı seçenekleri listelendi.";
        internal static string NumberOfRoomUpdated = "Oda sayısı seçeneği güncellendi";
        internal static string NumberOfRoomAlreadyExist = "Oda sayısı seçeneği daha önce eklenmiş";
        internal static string RoomCountIsRequired = "Oda sayısı girilmelidir.";
        internal static string RoomCountIsNotValid = "Oda sayısı en fazla 15 karakter olmalıdır.";
    }
}
