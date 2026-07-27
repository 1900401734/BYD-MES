using MesDatas.DatasModel;
using MesDatas.Utility.IniLaguagePath;
using MesDatas.Utility.ResourcesLaguage;
using MesDatas.Utility.SugarDB;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace MesDatas.DatasServer
{
    public class RecipeManage
    {
        // 初始化配方信息
        public static void InitCodes()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    db.CodeFirst.InitTables<RecipeEntity>();
                }
            }
            catch (Exception ex)
            {
            }
        }

        // 保存
        public static string GetCodesSave(RecipeEntity codes)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    if (db.Queryable<RecipeEntity>().Where(it => it.RecipeID == codes.RecipeID).Any())
                    {
                        return db.Updateable(codes).Where(it => it.RecipeID == codes.RecipeID).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                    else
                    {
                        return db.Insertable(codes).ExecuteCommand() > 0 ? LanguageResour.PassBtnSave : LanguageResour.ErrorBtnSave;
                    }
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnSave;
            }
        }

        // 修改
        public static string GetCodesUpdate(RecipeEntity codes)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Updateable(codes).ExecuteCommand() > 0 ? LanguageResour.PassBtnUpdate : LanguageResour.ErrorBtnUpdate;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnUpdate;
            }
        }

        // 删除
        public static string GetCodesDelete(RecipeEntity codes)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Deleteable(codes).Where(it => it.RecipeID == codes.RecipeID).ExecuteCommand() > 0 ? LanguageResour.PassBtnDelete : LanguageResour.ErrorBtnDelete;
                }
            }
            catch (Exception ex)
            {
                return LanguageResour.ErrorBtnDelete;
            }
        }

        // 获取
        public static RecipeEntity GetCodes(string id)
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<RecipeEntity>().Where(it => it.RecipeID == id).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取列表 
        public static List<RecipeEntity> GetCodesList()
        {
            try
            {
                using (var db = DBConnSugClie.GetDBConnection())
                {
                    return db.Queryable<RecipeEntity>().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 获取BindingList列表 
        public static BindingList<RecipeEntity> GetCodesBindingList()
        {
            return new BindingList<RecipeEntity>(GetCodesList());
        }
    }

    // <summary>
    /// 支持排序的BindingList包装器类
    /// </summary>
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor _sortProperty;

        public SortableBindingList() : base() { }

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;

        protected override bool IsSortedCore => _isSorted;

        protected override ListSortDirection SortDirectionCore => _sortDirection;

        protected override PropertyDescriptor SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            var items = this.Items as List<T>;
            if (items != null)
            {
                var comparer = new PropertyComparer<T>(prop, direction);
                items.Sort(comparer);
                _isSorted = true;
            }
            else
            {
                _isSorted = false;
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }
    }

    /// <summary>
    /// 属性比较器，用于排序
    /// </summary>
    public class PropertyComparer<T> : IComparer<T>
    {
        private readonly PropertyDescriptor _property;
        private readonly ListSortDirection _direction;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            _property = property;
            _direction = direction;
        }

        public int Compare(T x, T y)
        {
            object xValue = _property.GetValue(x);
            object yValue = _property.GetValue(y);

            if (xValue == null && yValue == null) return 0;
            if (xValue == null) return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (yValue == null) return _direction == ListSortDirection.Ascending ? 1 : -1;

            int result;
            if (xValue is IComparable comparableX)
            {
                result = comparableX.CompareTo(yValue);
            }
            else
            {
                result = string.Compare(xValue.ToString(), yValue.ToString(), StringComparison.CurrentCulture);
            }

            return _direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}
