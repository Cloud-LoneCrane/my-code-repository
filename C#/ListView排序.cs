ȫ����ͬ��Ŀ¼�´��� 
1.����ListViewColumnSorter�࣬������дListView���� 
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
                ///   ָ�������ĸ������� 
                ///   </summary> 
                private   int   ColumnToSort; 
                /**/ 
                ///   <summary> 
                ///   ָ������ķ�ʽ 
                ///   </summary> 
                private   SortOrder   OrderOfSort; 
                /**/ 
                ///   <summary> 
                ///   ����CaseInsensitiveComparer����� 
                ///   �μ�ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html/frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm 
                ///   </summary> 
                private   CaseInsensitiveComparer   ObjectCompare; 
                /**/ 
                ///   <summary> 
                ///   ���캯�� 
                ///   </summary> 
                public   ListViewColumnSorter() 
                { 
                        //   Ĭ�ϰ���һ������ 
                        ColumnToSort   =   0; 

                        //   ����ʽΪ������ 
                        OrderOfSort   =   SortOrder.None; 

                        //   ��ʼ��CaseInsensitiveComparer����� 
                        ObjectCompare   =   new   CaseInsensitiveComparer(); 
                } 

                /**/ 
                ///   <summary> 
                ///   ��дIComparer�ӿ�. 
                ///   </summary> 
                ///   <param   name= "x "> Ҫ�Ƚϵĵ�һ������ </param> 
                ///   <param   name= "y "> Ҫ�Ƚϵĵڶ������� </param> 
                ///   <returns> �ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1 </returns> 

                #region   IComparer   ��Ա 

                public   int   Compare(object   x,   object   y) 
                { 
                        int   compareResult; 
                        ListViewItem   listviewX,   listviewY; 

                        //   ���Ƚ϶���ת��ΪListViewItem���� 
                        listviewX   =   (ListViewItem)x; 
                        listviewY   =   (ListViewItem)y; 

                        //   �Ƚ� 
                        compareResult   =   ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text,   listviewY.SubItems[ColumnToSort].Text); 

                        //   ��������ıȽϽ��������ȷ�ıȽϽ�� 
                        if   (OrderOfSort   ==   SortOrder.Ascending) 
                        { 
                                //   ��Ϊ��������������ֱ�ӷ��ؽ�� 
                                return   compareResult; 
                        } 
                        else   if   (OrderOfSort   ==   SortOrder.Descending) 
                        { 
                                //   ����Ƿ�����������Ҫȡ��ֵ�ٷ��� 
                                return   (-compareResult); 
                        } 
                        else 
                        { 
                                //   �����ȷ���0 
                                return   0; 
                        } 
                } 
                #endregion 

                ////   <summary> 
                ///   ��ȡ�����ð�����һ������. 
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
                ///   ��ȡ����������ʽ. 
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
2.����Order�� 
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
                                //   �������ô��е����򷽷�. 
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
                                //   ���������У�Ĭ��Ϊ�������� 
                                lvwColumnSorter.SortColumn   =Column; 
                                lvwColumnSorter.Order   =   SortOrder.Ascending; 
                        } 
                        lv.ListViewItemSorter   =   lvwColumnSorter; 
                        lv.Sort(); 
                        lv.Sorting   =   lvwColumnSorter.Order; 
                } 
        } 
} 
3.��listView1��ColumnClick�¼���������´��룺   
        Order.OrderBy(sender,   e.Column); 
ע���˷���Ϊlistviewͨ�÷����� 
��columnClick�¼������Order.OrderBy(sender,   e.Column);�Ϳ���ʹ�ã������޸Ĵ��룬��Order����·����ȷ��