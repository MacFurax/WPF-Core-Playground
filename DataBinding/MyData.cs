using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DataBinding
{
	public class DataItem
	{
		private int id;
		private string path;
		private float value;

		public DataItem()
		{
		}

		public DataItem(int id, string path, float value)
		{
			this.id = id;
			this.path = path;
			this.value = value;
		}

		public int Id { get => id; set => id = value; }
		public string Path { get => path; set => path = value; }
		public float Value { get => value; set => this.value = value; }

		public override string ToString()
		{
			return "" + id + " " + path + " " + value ;
		}
	}

	public class MyData
	{
		public MyData()
		{
			_collection.Add(new DataItem(0, "/", 0.1f));
			_collection.Add(new DataItem(1, "/one/", 0.2f));
			_collection.Add(new DataItem(2, "/two/", 0.3f));
		}

		private string _colorName = "Red";
		private ObservableCollection<DataItem> _collection = new ObservableCollection<DataItem>();
		

		public string ColorName { get => _colorName; set => _colorName = value; }
		public ObservableCollection<DataItem> MyCollection { get => _collection; set => _collection = value; }
	}
}
