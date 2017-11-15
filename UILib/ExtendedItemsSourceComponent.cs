using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UILib
{
    public class ExtendedItemsSourceComponent
    {
        public delegate void ItemsSourceChangedHandler(IEnumerable OldSource, IEnumerable NewSource);
        public event ItemsSourceChangedHandler ItemsSourceChanged;
        public delegate void ItemsSourceCollectionChangedHandler(bool Add, object Object);
        public event ItemsSourceCollectionChangedHandler ItemsSourceCollectionChanged;

        private IEnumerable _ItemsSource = default(IEnumerable);

        public IEnumerable ItemsSource
        {
            get
            {
                return _ItemsSource;
            }

            set
            {
                ItemsSourceChanged?.Invoke(_ItemsSource, value);
                _ItemsSource = value;
                if (value is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged)_ItemsSource).CollectionChanged += ExtendedItemsSource_CollectionChanged;
                }
            }
        }

        private void ExtendedItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (object item in e.OldItems)
                {
                    ItemsSourceCollectionChanged?.Invoke(false, item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (object item in e.NewItems)
                {
                    ItemsSourceCollectionChanged?.Invoke(true, item);
                }
            }
        }
    }

    //public class ExtendedItemsSource
    //{
    //    public IEnumerable _ItemsSource;
    //    public IEnumerable ItemsSource
    //    {
    //        get { return _ItemsSource; }
    //        set
    //        {
    //            if (_ItemsSource != null)
    //                foreach (var item in _ItemsSource)
    //                {
    //                    this.ItemsChanged?.Invoke(false, item);
    //                }
    //            foreach (var item in value)
    //            {
    //                this.ItemsChanged?.Invoke(true, item);
    //            }
    //            _ItemsSource = value;
    //            if (value is INotifyCollectionChanged)
    //            {
    //                ((INotifyCollectionChanged)_ItemsSource).CollectionChanged += ExtendedItemsSource_CollectionChanged;
    //            }
    //            else
    //            {

    //            }

    //        }
    //    }

    //    private void ExtendedItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    //    {
    //        if (e.NewItems != null)
    //            foreach (var item in e.NewItems)
    //            {
    //                this.ItemsChanged?.Invoke(true, item);
    //            }
    //        if (e.OldItems != null)
    //            foreach (var item in e.OldItems)
    //            {
    //                this.ItemsChanged?.Invoke(false, item);
    //            }
    //    }

    //    public delegate void ItemsChangedHandler(bool AddOrRemove, object item);
    //    public event ItemsChangedHandler ItemsChanged;

    //    public ExtendedItemsSource()
    //    {

    //    }
    //}

}
