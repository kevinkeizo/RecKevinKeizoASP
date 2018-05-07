using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RecKevinKeizoKASP.ClassHelper
{
    public class FilesHelper
    {
        /// <summary>
        /// Metodo de Upload de Fotos, Para Minificação do Comandos, Quando Solicitado
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string name)
        {
            string path = string.Empty;
            //string pic = string.Empty;

            if (file == null || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
            {
                return false;
            }


            try
            {
                if (file != null)
                {
                    //pic = Path.GetFileName(file.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }



        }
        /// <summary>
        /// Metodo de Deletar Photo
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool DeletePhoto(string folder, string name)
        {

            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
            {
                return false;
            }

            //FileInfo userphoto = new FileInfo(pathfile);

            try
            {
                string path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                File.Delete(path);
                return true;
            }

            catch (Exception)
            {
                return false;

            }

        }

    }
}