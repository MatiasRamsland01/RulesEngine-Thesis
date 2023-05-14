using System;
using System.Collections;


namespace fields.Entities.Base.Fields.Base {

  public interface IAllowedType { }


  public class CustomCollection<T> : IEnumerable<T> where T : IAllowedType {
    private readonly List<T> _internalList = new List<T>();

    public void Add(T item) {
      _internalList.Add(item);
    }

    public IEnumerator<T> GetEnumerator() {
      return _internalList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }
  }
}
