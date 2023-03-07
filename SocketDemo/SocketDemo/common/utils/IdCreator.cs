///-------------------------------------------------------------------------------------------------
/// Copyright (c) 2023 WEI.ZHOU. All rights reserved.
/// The following code is only used for learning and communication, not for illegal and
/// commercial use.
/// If the code is used, no consent is required, but the author has nothing to do with any problems
/// and consequences.
/// In case of code problems, feedback can be made through the following email address.
/// 
///                        <wei.zhou@ccssttcn.com>
///------------------------------------------------------------------------------------------------- 
using System;

namespace SocketDemo.common.utils {

    public class IdCreator {
        private static readonly Random r = new Random();
        private static readonly IdCreator _default = new IdCreator();
        //实例编号
        private readonly long _instanceID;
        //索引可用位数
        private readonly int _indexBitLength;
        //时间戳最大值
        private readonly long _tsMax = 0;
        private readonly long _indexMax = 0;
        private readonly object _m_lock = new object();
        //当前时间戳
        private long _timestamp = 0;
        //索引/计数器
        private long _index = 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="instanceID">实例编号(0-1023)</param>
        /// <param name="indexBitLength">索引可用位数(1-32).每秒可生成ID数等于2的indexBitLength次方.大并发情况下,当前秒内ID数达到最大值时,将使用下一秒的时间戳,不影响获取ID.</param>
        /// <param name="initTimestamp">初始化时间戳,精确到秒.当之前同一实例生成ID的timestamp值大于当前时间的时间戳时,
        /// 有可能会产生重复ID(如持续一段时间的大并发请求).设置initTimestamp比最后的时间戳大一些,可避免这种问题</param>
        public IdCreator(int instanceID, int indexBitLength, long? initTimestamp = null) {
            if (instanceID < 0) {
                //这里给每个实例随机生成个实例编号
                this._instanceID = r.Next(0, 1024);
            } else {
                this._instanceID = instanceID % 1024;
            }
            if (indexBitLength < 1) {
                this._indexBitLength = 1;
            } else if (indexBitLength > 32) {
                this._indexBitLength = 32;
            } else {
                this._indexBitLength = indexBitLength;
            }
            _tsMax = Convert.ToInt64(new string('1', 53 - indexBitLength), 2);
            _indexMax = Convert.ToInt64(new string('1', indexBitLength), 2);
            if (initTimestamp != null) {
                this._timestamp = initTimestamp.Value;
            }
        }

        /// <summary>
        /// 默认每实例每秒生成65536个ID,从1970年1月1日起,累计可使用4358年
        /// </summary>
        /// <param name="instanceID">实例编号(0-1023)</param>
        public IdCreator(int instanceID) : this(instanceID, 16) {
        }

        /// <summary>
        /// 默认每秒生成65536个ID,从1970年1月1日起,累计可使用4358年
        /// </summary>
        public IdCreator() : this(-1) {
        }

        /// <summary>
        /// 生成64位ID
        /// </summary>
        /// <returns></returns>
        public long Create() {
            long id = 0;
            lock (_m_lock) {
                //增加时间戳部分
                long ts = GetTimeStamp();
                //如果超过时间戳允许的最大值,从0开始
                ts = ts % _tsMax;
                //腾出后面部分,给实例编号和索引编号使用
                id = ts << (10 + _indexBitLength);
                //增加实例部分
                id = id | (_instanceID << _indexBitLength);
                //获取计数
                if (_timestamp < ts) {
                    _timestamp = ts;
                    _index = 0;
                } else {
                    if (_index > _indexMax) {
                        _timestamp++;
                        _index = 0;
                    }
                }
                id = id | _index;
                _index++;
            }
            return id;
        }

        /// <summary>
        /// 获取当前实例的时间戳
        /// </summary>
        public long CurrentTimestamp {
            get {
                return this._timestamp;
            }
        }

        /// <summary>
        /// 默认每实例每秒生成65536个ID,从1970年1月1日起,累计可使用4358年
        /// </summary>
        public static IdCreator Default {
            get {
                return _default;
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns>long类型的时间戳</returns>
        public long GetTimeStamp() {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}
