using System;

namespace L2_login
{
	public class BaseTargetClass
	{
		protected bool _Active;
		private readonly object ActiveLock = new object();

		public bool Active
		{
			get
			{
				bool tmp;
				lock(ActiveLock)
				{
					tmp = this._Active;
				}
				return tmp;
			}
			set
			{
				lock(ActiveLock)
				{
					_Active = value;
				}
			}
		}
	}
}
