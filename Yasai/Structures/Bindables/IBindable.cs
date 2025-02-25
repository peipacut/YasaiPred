﻿using System;

namespace Yasai.Structures.Bindables 
{
    /**
     * generic form of a data structure which can bind to something else
     */
    public interface IBindable <T>
    {
        T Value { get; set; }
        
        IBindable<T> Dependency { get; }
        
        BindStatus BindStatus { get; }

        /// <summary>
        /// Bind to another bindable in a bidirectional fashion,
        /// initially the current bindable takes precedence over the bound value
        /// </summary>
        /// <param name="other">the object to share values with</param>
        void Bind(IBindable<T> other);
        
        /// <summary>
        /// Bind to another bindable in a unidirectional fashion.
        /// In this relationship, this bindable is readonly and only contains
        /// the values of its master
        /// </summary>
        /// <param name="master">the bindable to read values from</param>
        void BindTo(IBindable<T> master);

        IBindable<String> ToBindableString();
        
        /// <summary>
        /// Unbind this bindable from all other bindables, this is implicitly called
        /// every time this bindable has been binded
        /// </summary>
        void Unbind();
        
        /// <summary>
        /// Value of bindable has been changed
        /// </summary>
        event Action<T> OnSet;
        
        /// <summary>
        /// Value of bindable has mutated
        /// </summary>
        event Action<T> OnChanged;
        
        /// <summary>
        /// Value of bindable has been retrieved
        /// </summary>
        event Action OnGet;
    }

    public enum BindStatus
    {
        Unidirectional,
        Bidirectional,
        Unbound
    }
}
