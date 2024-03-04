using ColorManager.DataBase.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ColorManager.DataBase.Queries
{
    public class PallettersQuery
    {
        /// <summary>
        /// Возвращает все элементы таблицы Palletters
        /// </summary>
        /// <returns>palletters при успешном поиске эелементов/null при не нахождении эелементов</returns>
        public List<Palletters> GetAll()
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    List<Palletters> palletters = new List<Palletters>();

                    foreach (Palletters palletter in db.Palletters)
                    {
                        foreach (Palletters palletter1 in palletters)
                        {
                            if (palletter1 == palletter) break;
                            else
                            {
                                palletters.Add(palletter);
                                break;
                            }
                        }
                    }
                    return palletters;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// <summary>
        /// Возвращает имя продукта
        /// </summary>
        /// <returns>List строковых значений с именем продукта</returns>
        public List<string> GetProductGroup()
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    List<string> productGroup = new List<string>();

                    foreach (Palletters palletter in db.Palletters)
                    {
                        foreach (string group in productGroup)
                        {
                            if (group == palletter.ProductGroup) break;
                            else
                            {
                                productGroup.Add(palletter.ProductGroup);
                                break;
                            }
                        }
                    }
                    return productGroup;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// <summary>
        /// Возвращает цветовую гамму продукта
        /// </summary>
        /// <param name="productGroup">Имя продукта</param>
        /// <returns>List строковых значений по имени продукта</returns>
        public List<string> GetColorFan(string productGroup)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    List<string> colorFan = new List<string>();

                    foreach (Palletters palletter in db.Palletters)
                    {
                        if (palletter.ProductGroup == productGroup)
                            colorFan.Add(palletter.ColorFan);
                    }
                    return colorFan;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// <summary>
        /// Возвращает цвета продукта
        /// </summary>
        /// <param name="productGroup">Имя продукта</param>
        /// <param name="colorFan">Цветовая гамма</param>
        /// <returns>List строковых значений по имени и цветовой гамме продукта</returns>
        public List<string> GetColor(string productGroup, string colorFan)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    List<string> color = new List<string>();

                    foreach (Palletters palletter in db.Palletters)
                    {
                        if (palletter.ProductGroup == productGroup && palletter.ColorFan == colorFan)
                            color.Add(palletter.Color);
                    }
                    return color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// <summary>
        /// Возвращает величену цвета
        /// </summary>
        /// <param name="productGroup">Имя продукта</param>
        /// <param name="colorFan">Цветовая гамма</param>
        /// <param name="color">Цвет продукта</param>
        /// <returns>Строка с величиной цвета</returns>
        public string GetColorValue(string productGroup, string colorFan, string color)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    foreach (Palletters palletter in db.Palletters)
                    {
                        if (palletter.ProductGroup == productGroup && palletter.ColorFan == colorFan && palletter.Color == color)
                        {
                            color = palletter.Color;
                            break;
                        }
                    }
                    return color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        
        /// <summary>
        /// Возвращает наличие продукта
        /// </summary>
        /// <param name="productGroup">Имя продукта</param>
        /// <param name="colorFan">Цветовая гамма</param>
        /// <param name="color">Цвет продукта</param>
        /// <param name="colorValue">Величина цвета</param>
        /// <returns>true при наличии продукта/false при его отсутствии</returns>
        public bool GetInStockInfo(string productGroup, string colorFan, string color, string colorValue)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    bool inStockInfo = false;
                    foreach (Palletters palletter in db.Palletters)
                    {
                        if (palletter.ProductGroup == productGroup && palletter.ColorFan == colorFan && palletter.Color == color && palletter.ColorValue == colorValue)
                        {
                            inStockInfo = palletter.InStock;
                            break;
                        }
                    }
                    return inStockInfo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
