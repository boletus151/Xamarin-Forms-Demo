﻿/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="DynamicListView" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is a group of the offers of several persons along the network;
    because of this, it is under Creative Common By License:

    You are free to:

    Share — copy and redistribute the material in any medium or format
    Adapt — remix, transform, and build upon the material for any purpose, even commercially.

    The licensor cannot revoke these freedoms as long as you follow the license terms.

    Under the following terms:

    Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
    No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.

 </summary>
--------------------------------------------------------------------------------------------------------------------*/

namespace XFDemo.CustomControls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class DynamicListView<T> : ListView where T : class
    {
        #region Private Fields

        private readonly ObservableCollection<T> observableList;

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [full items source changed].
        /// </summary>
        /// <param name="bindableElement">The bindable element.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnFullItemsSourceChanged(BindableObject bindableElement, object oldvalue, object newvalue)
        {
            var listView = bindableElement as DynamicListView<T>;
            listView?.OnFullItemsSourceChanged(listView);
        }

        /// <summary>
        /// Called when [full items source changed].
        /// </summary>
        /// <param name="listView">The list view.</param>
        private void OnFullItemsSourceChanged(DynamicListView<T> listView)
        {
            if (listView.FullItemsSource != null)
            {
                var numberOfItems = int.Parse(listView.ItemsPerPage);

                this.observableList.Clear();


                if (numberOfItems > listView.FullItemsSource.Count)
                {
                    numberOfItems = listView.FullItemsSource.Count;
                }
                var items = listView.FullItemsSource.ToList().GetRange(0, numberOfItems);
                foreach (var elem in items)
                {
                    this.observableList.Add(elem);
                }
            }
        }

        /// <summary>
        /// Called when [item appearing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="ItemVisibilityEventArgs"/> instance containing the event data.
        /// </param>
        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listView = sender as DynamicListView<T>;
            if (listView?.FullItemsSource == null || this.observableList.Count == listView.FullItemsSource.Count)
            {
                return;
            }

            var indexOfItem = this.observableList.IndexOf((T)e.Item);
            var numberOfItems = int.Parse(listView.ItemsPerPage);

            if (indexOfItem > -1 && e.Item == this.observableList.LastOrDefault())
            {
                var observableItemsCount = this.observableList.Count;
                var fullItemsCount = listView.FullItemsSource.Count;

                if (numberOfItems > listView.FullItemsSource.Count)
                {
                    numberOfItems = listView.FullItemsSource.Count;
                }

                if (numberOfItems > fullItemsCount - observableItemsCount)
                {
                    numberOfItems = fullItemsCount - observableItemsCount;
                }

                var items = listView.FullItemsSource.ToList().GetRange(this.observableList.Count, numberOfItems);
                foreach (var elem in items)
                {
                    this.observableList.Add(elem);
                }
            }
        }

        #endregion

        #region Public Fields

        /// <summary>
        /// The full items source property
        /// </summary>
        public static BindableProperty FullItemsSourceProperty = BindableProperty.Create
            (nameof(FullItemsSource), typeof(IList<T>), typeof(DynamicListView<T>), default(IList<T>), BindingMode.OneWay, null,
                OnFullItemsSourceChanged);

        /// <summary>
        /// The items per page property
        /// </summary>
        public static BindableProperty ItemsPerPageProperty = BindableProperty.Create
            (nameof(ItemsPerPage), typeof(string), typeof(DynamicListView<T>), string.Empty);

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicListView{T}"/> class.
        /// </summary>
        public DynamicListView()
        {
            this.observableList = new ObservableCollection<T>();
            this.ItemsSource = this.observableList;
            this.ItemAppearing += this.OnItemAppearing;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the full items source.
        /// </summary>
        /// <value>The full items source.</value>
        public IList<T> FullItemsSource
        {
            get
            {
                return (IList<T>)this.GetValue(FullItemsSourceProperty);
            }
            set
            {
                this.SetValue(FullItemsSourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the page, i.e the number of items per load.
        /// </summary>
        /// <value>The size of the page.</value>
        public string ItemsPerPage
        {
            get
            {
                return (string)this.GetValue(ItemsPerPageProperty);
            }
            set
            {
                this.SetValue(ItemsPerPageProperty, value);
            }
        }

        #endregion
    }
}