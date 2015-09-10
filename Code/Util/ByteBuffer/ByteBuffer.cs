//#define ERROR

using System;

namespace L2_login
{
	public class ByteBuffer
	{
		private const int MAX_LENGTH = 1024;
		private byte[] _data;
		private int _length;
		private int _maxlength;
		private int _index;

		public ByteBuffer(int len = MAX_LENGTH)
		{
            if (len > _maxlength)
            {
                _maxlength = len;
            }
            else
            {
                _maxlength = MAX_LENGTH;
            }

			_data = new byte[_maxlength];
			_length = len;
			_index = 0;
		}

		public ByteBuffer(byte[] buff)
		{
			_length = buff.Length;

			if(_length > _maxlength)
			{
				_maxlength = _length;
			}

			_data = new byte[_maxlength];
			//_length = //already set
			_index = 0;

            buff.CopyTo(_data, 0);
		}

        public ByteBuffer(ByteBuffer buff)
        {
            _length = buff._length;
            _maxlength = buff._maxlength;

            _data = new byte[_maxlength];
            _index = 0;

            buff._data.CopyTo(_data, 0);
        }

		public int Length()
		{
			return _length;
		}

		public void Resize(int len)
		{
			if(len <= _maxlength)
			{
				//the new length is less than or equal to our maxlength
				_length = len;
			}
			if(len > _maxlength)
			{
				//the new length is larger than our max length so we need to resize accordingly

				//make a temp array of the length of our data
				byte[] _copy = new byte[_length];

				//copy our data over
                _data.CopyTo(_copy, 0);

				//set the new max length
				_maxlength = len;

				//remake our data array
				_data = new byte[_maxlength];

				//copy data back in up to length(still the size of the old data set)
                _copy.CopyTo(_data, 0);

				//set length to our max length
				_length = _maxlength;
			}
		}

		public void ResetIndex()
		{
			_index = 0;
		}

		public void ClearData()
		{
			for(int i = 0; i < _maxlength; i ++)
			{
				_data[i] = (byte)(0);
			}
		}

		//read data

		public UInt16 ReadUInt16()
		{
			if(_length >= _index + 2)
			{
				UInt16 temp = System.BitConverter.ToUInt16(_data,_index);
				_index += 2;
				return temp;
			}

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadUInt16");
#endif
			return 0;
		}

		public UInt32 ReadUInt32()
		{
			if(_length >= _index + 4)
			{
				UInt32 temp = System.BitConverter.ToUInt32(_data,_index);
				_index += 4;
				return temp;
            }

#if DEBUG
            Globals.l2net_home.Add_Error("read beyond array size ReadUInt32");
#endif
			return 0;
		}

		public UInt64 ReadUInt64()
		{
			if(_length >= _index + 8)
			{
				UInt64 temp = System.BitConverter.ToUInt64(_data,_index);
				_index += 8;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadUInt64");
#endif
			return 0;
		}
		public Int16 ReadInt16()
		{
			if(_length >= _index + 2)
			{
				Int16 temp = System.BitConverter.ToInt16(_data,_index);
				_index += 2;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadInt16");
#endif
			return 0;
		}

		public Int32 ReadInt32()
		{
			if(_length >= _index + 4)
			{
				Int32 temp = System.BitConverter.ToInt32(_data,_index);
				_index += 4;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadInt32");
#endif
			return 0;
		}

		public Int64 ReadInt64()
		{
			if(_length >= _index + 8)
			{
				Int64 temp = System.BitConverter.ToInt64(_data,_index);
				_index += 8;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadInt64");
#endif
			return 0;
		}

		public double ReadDouble()
		{
			if(_length >= _index + 8)
			{
				double temp = System.BitConverter.ToDouble(_data,_index);
				_index += 8;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadDouble");
#endif
			return 0;
		}

		public char ReadChar()//2 bytes, unicode, use ReadBtye for 1 byte
		{
			if(_length >= _index + 2)
			{
				char temp = System.BitConverter.ToChar(_data,_index);
				_index += 2;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadChar");
#endif
			return (char)0;
		}

		public byte ReadByte()
		{
			if(_length >= _index + 1)
			{
				byte temp = _data[_index];
				_index += 1;
				return temp;
            }

#if DEBUG && ERROR
            Globals.l2net_home.Add_Error("read beyond array size ReadByte");
#endif
			return (byte)0;
		}

		public string ReadString()
		{
			try
			{
				string tmp = "";

                char tmp2 = ReadChar();
				while(tmp2 != 0x00)
				{
					tmp += tmp2;
                    tmp2 = ReadChar();
				}

				return tmp;
			}
			catch
			{
				return "";
			}
		}

        public string ReadString1B()
        {
            try
            {
                string tmp = "";

                char tmp2 = (char)ReadByte();
                while (tmp2 != 0x00)
                {
                    tmp += tmp2;
                    tmp2 = (char)ReadByte();
                }

                return tmp;
            }
            catch
            {
                return "";
            }
        }

		//write values

		public void WriteUInt16(UInt16 val)
		{
			if(_length >= _index + 2)
			{
				byte[] tmp = new byte[2];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];

				_index += 2;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteUInt16");
			}
#endif
		}

		public void WriteUInt32(UInt32 val)
		{
			if(_length >= _index + 4)
			{
				byte[] tmp = new byte[4];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];
				_data[_index+2] = tmp[2];
				_data[_index+3] = tmp[3];

				_index += 4;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteUInt32");
			}
#endif
		}

		public void WriteUInt64(UInt64 val)
		{
			if(_length >= _index + 8)
			{
				byte[] tmp = new byte[8];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];
				_data[_index+2] = tmp[2];
				_data[_index+3] = tmp[3];
				_data[_index+4] = tmp[4];
				_data[_index+5] = tmp[5];
				_data[_index+6] = tmp[6];
				_data[_index+7] = tmp[7];

				_index += 8;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteUInt64");
			}
#endif
		}

		public void WriteInt16(Int16 val)
		{
			if(_length >= _index + 2)
			{
				byte[] tmp = new byte[2];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];

				_index += 2;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteInt16");
			}
#endif
		}

		public void WriteInt32(Int32 val)
		{
			if(_length >= _index + 4)
			{
				byte[] tmp = new byte[4];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];
				_data[_index+2] = tmp[2];
				_data[_index+3] = tmp[3];

				_index += 4;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteInt32");
			}
#endif
		}

		public void WriteInt64(Int64 val)
		{
			if(_length >= _index + 8)
			{
				byte[] tmp = new byte[8];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];
				_data[_index+2] = tmp[2];
				_data[_index+3] = tmp[3];
				_data[_index+4] = tmp[4];
				_data[_index+5] = tmp[5];
				_data[_index+6] = tmp[6];
				_data[_index+7] = tmp[7];

				_index += 8;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteInt64");
			}
#endif
		}

		public void WriteDouble(double val)
		{
			if(_length >= _index + 8)
			{
				byte[] tmp = new byte[8];
				tmp = System.BitConverter.GetBytes(val);

				_data[_index] = tmp[0];
				_data[_index+1] = tmp[1];
				_data[_index+2] = tmp[2];
				_data[_index+3] = tmp[3];
				_data[_index+4] = tmp[4];
				_data[_index+5] = tmp[5];
				_data[_index+6] = tmp[6];
				_data[_index+7] = tmp[7];

				_index += 8;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteDouble");
			}
#endif
		}

		public void WriteByte(byte val)
		{
			if(_length >= _index + 1)
			{
				_data[_index] = val;

				_index += 1;
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteByte");
			}
#endif
		}

        public void WriteBytes(byte[] val)
        {
            if (_length >= _index + val.Length)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    _data[_index] = val[i];

                    _index += 1;
                }
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteBytes");
			}
#endif
        }

		public void WriteString(string text)
		{
			if(_length >= _index + (text.Length * 2) + 2)
			{
                byte[] data = System.Text.Encoding.Unicode.GetBytes(text);//each char becomes 2 bytes
				
                for(int i = 0; i < data.Length; i++)
				{
					WriteByte( data[i] );
				}
				WriteByte(0x00);//null terminated string
				WriteByte(0x00);//
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteString");
			}
#endif
		}

        public void WriteString1B(string text)
        {
            if (_length >= _index + (text.Length) + 1)
            {
                byte[] data = System.Text.Encoding.UTF7.GetBytes(text);//each char becomes 2 bytes

                for (int i = 0; i < data.Length; i++)
                {
                    WriteByte(data[i]);
                }
                WriteByte(0x00);//null terminated string
            }
#if DEBUG && ERROR
            else
			{
                Globals.l2net_home.Add_Error("write beyond array size WriteString1B");
			}
#endif
        }

		//set values

		public byte GetByte(int ind)
		{
			if(_length >= ind)
			{
				return _data[ind];
			}

			return 0;
		}

		public void SetByte(int ind, byte b)
		{
			if(_length >= ind)
			{
				_data[ind] = b;
			}
		}

		public int GetIndex()
		{
			//returns the location of the current byte
			return _index;
		}

        public void TrimToIndex()
        {
            _length = _index;
        }

		public void SetIndex(int ind)
		{
			_index = ind;

#if DEBUG && ERROR
            if (_length < _index)
			{
                Globals.l2net_home.Add_Error("index set to beyond the length of the array");
			}
#endif
		}

		public byte[] Get_ByteArray()
		{
			byte[] tmp = new byte[_length];

			for(int i = 0; i < _length; i ++)
			{
				tmp[i] = _data[i];
			}

			return tmp;
		}

        public byte[] Get_ByteArray2()
        {
            byte[] tmp = new byte[_length - _index];

            for (int i = _index; i < _length; i++)
            {
                tmp[i - _index] = _data[i];
            }

            return tmp;
        }
	}//end of class
}
