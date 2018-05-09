using System;
using System.Collections;

namespace Nuevo
{
	public class BoidCollection : CollectionBase
	{
		public void Add(Boid s)
		{
			List.Add(s);
		}

		public void Remove(Boid s)
		{
			List.Remove(s);
		}

		public bool Contains(Boid s){
			return List.Contains (s);
		}

		public void AddBoids(BoidCollection c){
			foreach (Boid b in c)
				if (!List.Contains (b))
					List.Add (b);
		}

		public Boid this[int index]
		{
			get { return (Boid)List[index]; }
			set { List[index] = value; }
		}



	}
}

