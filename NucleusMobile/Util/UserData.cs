//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO.IsolatedStorage;
//using System.Collections.Specialized;
//using System.IO;

//namespace Nucleus
//{
//    public class UserData
//    {
//        public static void DeleteUserData()
//        {
//#if WP8
//            throw new NotImplementedException();

//#else
//            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
//            {
//                if (isoStore.FileExists("data.txt"))
//                {
//                    isoStore.DeleteFile("data.txt");
//                }
//            }
//#endif
//        }

//        public static Dictionary<string, string> LoadUserData()
//        {
//#if WP8
//            throw new NotImplementedException();

//#else
//            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
//            {
//                if (!isoStore.FileExists("data.txt"))
//                {
//                    return null;
//                }

//                Dictionary<string, string> data = new Dictionary<string, string>();

//                using (Stream s = isoStore.OpenFile("data.txt", FileMode.Open))
//                {
//                    using (StreamReader reader = new StreamReader(s))
//                    {
//                        while (!reader.EndOfStream)
//                        {
//                            data.Add(reader.ReadLine(), reader.ReadLine());
//                        }
//                    }
//                }
//                return data;
//            }
//#endif
//        }

//        public static void SaveUserData(Dictionary<string, string> data)
//        {
//#if WP8

//            throw new NotImplementedException();
//#else
//            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
//            {
//                if (isoStore.FileExists("data.txt"))
//                {
//                    isoStore.DeleteFile("data.txt");
//                }

//                using (Stream s = isoStore.CreateFile("data.txt"))
//                {
//                    using (StreamWriter writer = new StreamWriter(s))
//                    {
//                        foreach (var str in data)
//                        {
//                            writer.WriteLine(str.Key);
//                            writer.WriteLine(str.Value);
//                        }
//                    }
//                }
//            }
//#endif
//        }
//    }
//}