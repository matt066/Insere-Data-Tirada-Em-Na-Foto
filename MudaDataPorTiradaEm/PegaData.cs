using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Windows;  
using System.Linq;

namespace MudaDataPorTiradaEm
{
    class PegaData
    {
        /// <summary>
        /// Returns the EXIF Image Data of the Date Taken.
        /// </summary>
        /// <param name="getImage">Image (If based on a file use Image.FromFile(f);)</param>
        /// <returns>Date Taken or Null if Unavailable</returns>
        
        public DateTime? DateTaken(Image getImage)
        {
            int DateTakenValue = 0x9003; //36867 - Esse numero é referente a propriedade "Tirada em" da foto;

            if (!getImage.PropertyIdList.Contains(DateTakenValue))
                return null;

            string dateTakenTag = System.Text.Encoding.ASCII.GetString(getImage.GetPropertyItem(DateTakenValue).Value);
            string[] parts = dateTakenTag.Split(':', ' ');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);
            int hour = int.Parse(parts[3]);
            int minute = int.Parse(parts[4]);
            int second = int.Parse(parts[5]);

            return new DateTime(year, month, day, hour, minute, second);
        }


        public void ChangeDateTaken(string data, string path)
        {
            Image theImage = new Bitmap(path);
            PropertyItem[] propItems = theImage.PropertyItems;
            Encoding _Encoding = Encoding.UTF8;
            var DataTakenProperty1 = propItems.Where(a => a.Id.ToString("x") == "9004").FirstOrDefault();
            var DataTakenProperty2 = propItems.Where(a => a.Id.ToString("x") == "9003").FirstOrDefault();
            string originalDateString = _Encoding.GetString(DataTakenProperty1.Value);
            originalDateString = originalDateString.Remove(originalDateString.Length - 1);
            DateTime originalDate = DateTime.ParseExact(originalDateString, "yyyy:MM:dd HH:mm:ss", null);

            originalDate = originalDate.AddHours(-7);


            DataTakenProperty1.Value = _Encoding.GetBytes(originalDate.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
            DataTakenProperty2.Value = _Encoding.GetBytes(originalDate.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
            theImage.SetPropertyItem(DataTakenProperty1);
            theImage.SetPropertyItem(DataTakenProperty2);
            string new_path = System.IO.Path.GetDirectoryName(path) + "\\_" + System.IO.Path.GetFileName(path);
            theImage.Save(new_path);
            theImage.Dispose();
        }

        public void SetDateTaken(string data, string path)
        {
            Image theImage = new Bitmap(path);
            PropertyItem[] propItems = theImage.PropertyItems;
            Encoding _Encoding = Encoding.UTF8;
            var DataTakenProperty1 = propItems.FirstOrDefault();
            var DataTakenProperty2 = propItems.FirstOrDefault();

            string originalDateString = data + "\0";
            originalDateString = originalDateString.Remove(originalDateString.Length - 1);
            DateTime originalDate = DateTime.ParseExact(originalDateString, "yyyy:MM:dd HH:mm:ss", null);

            originalDate = originalDate.AddHours(-7);


            DataTakenProperty1.Value = _Encoding.GetBytes(originalDate.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
            DataTakenProperty1.Id = 36867;
            DataTakenProperty1.Len = 20;
            DataTakenProperty1.Type = 2;
            DataTakenProperty2.Value = _Encoding.GetBytes(originalDate.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
            DataTakenProperty2.Id = 36867;
            DataTakenProperty2.Len = 20;
            DataTakenProperty2.Type = 2;
            theImage.SetPropertyItem(DataTakenProperty1);
            theImage.SetPropertyItem(DataTakenProperty2);
            string new_path = System.IO.Path.GetDirectoryName(path) + "\\d_" + System.IO.Path.GetFileName(path);
            theImage.Save(new_path);
            theImage.Dispose();
        }
    }
}
