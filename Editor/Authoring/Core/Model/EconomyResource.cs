using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.Services.DeploymentApi.Editor;

namespace Unity.Services.Economy.Editor.Authoring.Core.Model
{
    class EconomyResource : IEconomyResource, IEquatable<EconomyResource>
    {
        float m_Progress;
        DeploymentStatus m_Status;

        const string k_ConfigType = "Economy Resource";
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual string EconomyType { get; }
        public object CustomData { get; set; }
        public virtual string EconomyExtension { get; set; }
        public virtual string Type => k_ConfigType;
        public string Path { get; set; }


        public float Progress
        {
            get => m_Progress;
            set => SetField(ref m_Progress, value);
        }


        public DeploymentStatus Status
        {
            get => m_Status;
            set => SetField(ref m_Status, value);
        }


        public ObservableCollection<AssetState> States { get; } = new ObservableCollection<AssetState>();

        public EconomyResource(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            if (Path == "Remote")
                return Id;
            return $"'{Path}'";
        }

        /// <summary>
        /// Event will be raised when a property of the instance is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the field and raises an OnPropertyChanged event.
        /// </summary>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="onFieldChanged">The callback.</param>
        /// <param name="propertyName">Name of the property to set.</param>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        protected void SetField<T>(
            ref T field,
            T value,
            Action<T> onFieldChanged = null,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            onFieldChanged?.Invoke(field);
        }

        public bool Equals(EconomyResource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EconomyResource)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
