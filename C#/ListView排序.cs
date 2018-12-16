全部在同级目录下创建 
1.创建ListViewColumnSorter类，作用重写ListView排序 
using   System; 
using   System.Collections.Generic; 
using   System.Text; 
using   System.Collections; 
using   System.Windows.Forms; 

namespace   MotoExam.UserHelp 
{ 
        public   class   ListViewColumnSorter   :   IComparer 
        { 
                ///   <summary> 
                ///   指定按照哪个列排序 
                ///   </summary> 
                private   int   ColumnToSort; 
                /**/ 
                ///   <summary> 
                ///   指定排序的方式 
                ///   </summary> 
                private   SortOrder   OrderOfSort; 
                /**/ 
                ///   <summary> 
                ///   声明CaseInsensitiveComparer类对象， 
                ///   参见ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html/frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm 
                ///   </summary> 
                private   CaseInsensitiveComparer   ObjectCompare; 
                /**/ 
                ///   <summary> 
                ///   构造函数 
                ///   </summary> 
                public   ListViewColumnSorter() 
                { 
                        //   默认按第一列排序 
                        ColumnToSort   =   0; 

                        //   排序方式为不排序 
                        OrderOfSort   =   SortOrder.None; 

                        //   初始化CaseInsensitiveComparer类对象 
                        ObjectCompare   =   new   CaseInsensitiveComparer(); 
                } 

                /**/ 
                ///   <summary> 
                ///   重写IComparer接口. 
                ///   </summary> 
                ///   <param   name= "x "> 要比较的第一个对象 </param> 
                ///   <param   name= "y "> 要比较的第二个对象 </param> 
                ///   <returns> 比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1 </returns> 

                #region   IComparer   成员 

                public   int   Compare(object   x,   object   y) 
                { 
                        int   compareResult; 
                        ListViewItem   listviewX,   listviewY; 

                        //   将比较对象转换为ListViewItem对象 
                        listviewX   =   (ListViewItem)x; 
                        listviewY   =   (ListViewItem)y; 

                        //   比较 
                        compareResult   =   ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text,   listviewY.SubItems[ColumnToSort].Text); 

                        //   根据上面的比较结果返回正确的比较结果 
                        if   (OrderOfSort   ==   SortOrder.Ascending) 
                        { 
                                //   因为是正序排序，所以直接返回结果 
                                return   compareResult; 
                        } 
                        else   if   (OrderOfSort   ==   SortOrder.Descending) 
                        { 
                                //   如果是反序排序，所以要取负值再返回 
                                return   (-compareResult); 
                        } 
                        else 
                        { 
                                //   如果相等返回0 
                                return   0; 
                        } 
                } 
                #endregion 

                ////   <summary> 
                ///   获取或设置按照哪一列排序. 
                ///   </summary> 
                public   int   SortColumn 
                { 
                        set 
                        { 
                                ColumnToSort   =   value; 
                        } 
                        get 
                        { 
                                return   ColumnToSort; 
                        } 
                } 

                /**/ 
                ///   <summary> 
                ///   获取或设置排序方式. 
                ///   </summary> 
                public   SortOrder   Order 
                { 
                        set 
                        { 
                                OrderOfSort   =   value; 
                        } 
                        get 
                        { 
                                return   OrderOfSort; 
                        } 
                } 
        } 
} 
2.创建Order类 
using   System; 
using   System.Collections.Generic; 
using   System.Text; 
using   System.Windows.Forms; 

namespace   MotoExam.UserHelp 
{ 
        public   class   Order 
        { 
                public   static   void   OrderBy(Object   sender,int   Column) 
                { 
                        ListView   lv   =   sender   as   ListView; 
                        ListViewColumnSorter   lvwColumnSorter   =   new   ListViewColumnSorter(); 
                        lvwColumnSorter.SortColumn   =   Column; 
                        if   (Column   ==   lvwColumnSorter.SortColumn) 
                        { 
                                lvwColumnSorter.Order   =   lv.Sorting; 
                                //   重新设置此列的排序方法. 
                                if   (lvwColumnSorter.Order   ==   SortOrder.Ascending) 
                                { 
                                        lvwColumnSorter.Order   =   SortOrder.Descending; 
                                } 
                                else 
                                { 
                                        lvwColumnSorter.Order   =   SortOrder.Ascending; 
                                } 
                        } 
                        else 
                        { 
                                //   设置排序列，默认为正向排序 
                                lvwColumnSorter.SortColumn   =Column; 
                                lvwColumnSorter.Order   =   SortOrder.Ascending; 
                        } 
                        lv.ListViewItemSorter   =   lvwColumnSorter; 
                        lv.Sort(); 
                        lv.Sorting   =   lvwColumnSorter.Order; 
                } 
        } 
} 
3.在listView1的ColumnClick事件中添加以下代码：   
        Order.OrderBy(sender,   e.Column); 
注：此方法为listview通用方法， 
在columnClick事件中添加Order.OrderBy(sender,   e.Column);就可以使用，无需修改代码，但Order方法路径正确。