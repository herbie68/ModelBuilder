using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Modelbuilder;
public class SumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
    {
        var sum = 0.0;
        Type valueType = value.GetType();

        if (valueType.Name == typeof(List<>).Name)
        {
            foreach (var item in (IList)value)
            {
                Type itemType = item.GetType();
                PropertyInfo itemPropertyInfo = itemType.GetProperty((string)parameter);
                double itemValue = (double)itemPropertyInfo.GetValue(item, null);
                sum += itemValue;
            }
            return string.Concat((System.Convert.ToInt32(sum) / 60).ToString(), ":", ("00" + System.Convert.ToInt32(sum) % 60).AsSpan(("00" + System.Convert.ToInt32(sum) % 60).Length - 2, 2));
        }

        if (value is ReadOnlyObservableCollection<object>)
        {
            var items = (ReadOnlyObservableCollection<object>)value;
            foreach (var item1 in items)
            {
                if (item1.GetType().Name == "CollectionViewGroupInternal") // If Group is subgroup items are stored in Internal CollectionView
                {
                    CollectionViewGroup cvg1 = item1 as CollectionViewGroup;
                    foreach(var item2 in cvg1.Items)
                    {
                        if (item2.GetType().Name == "CollectionViewGroupInternal")
                        {
                            CollectionViewGroup cvg2 = item2 as CollectionViewGroup;
                            foreach (var item3 in cvg2.Items)
                            {
                                if (item3.GetType().Name == "CollectionViewGroupInternal")
                                {
                                    CollectionViewGroup cvg3 = item3 as CollectionViewGroup;
                                    foreach (var item4 in cvg3.Items)
                                    {
                                        if (item4.GetType().Name == "CollectionViewGroupInternal")
                                        {
                                            CollectionViewGroup cvg4 = item4 as CollectionViewGroup;

                                            foreach (var item5 in cvg4.Items)
                                            {
                                                DataRowView drv = item5 as DataRowView;
                                                sum += double.Parse(drv[9].ToString());
                                            }
                                        }
                                        else
                                        {
                                            DataRowView drv = item4 as DataRowView;
                                            sum += double.Parse(drv[9].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    DataRowView drv = item3 as DataRowView;
                                    sum += double.Parse(drv[9].ToString());
                                }
                            }
                        }
                        else
                        {
                            DataRowView drv = item2 as DataRowView;
                            sum += double.Parse(drv[9].ToString());
                        }
                    }
                }
                else
                {
                    DataRowView drv = item1 as DataRowView;
                    sum += double.Parse(drv[9].ToString());
                }
            }
            return string.Concat((System.Convert.ToInt32(sum) / 60).ToString(), ":", ("00" + System.Convert.ToInt32(sum) % 60).AsSpan(("00" + System.Convert.ToInt32(sum) % 60).Length - 2, 2));
        }
        return 0.0;
    }
    public object ConvertBack(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}