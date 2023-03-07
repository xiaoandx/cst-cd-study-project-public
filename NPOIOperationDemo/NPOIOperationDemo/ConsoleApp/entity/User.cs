/*
 * Copyright (c) 2023 WEI.ZHOU. All rights reserved.
 * The following code is only used for learning and communication, not for illegal and
 * commercial use.
 * If the code is used, no consent is required, but the author has nothing to do with any problems
 * and consequences.
 * In case of code problems, feedback can be made through the following email address.
 *
 *                        <wei.zhou@ccssttcn.com>
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.entity
{
    public class User {
        [Description("学号")]
        public string ID { get; set; }

        [Description("姓名")]
        public string Name { get; set; }

        [Description("年龄")]
        public string Age { get; set; }

        [Description("性别")]
        public string Six { get; set; }

        [Description("地址")]
        public string Address { get; set; }

        [Description("电话")]
        public string Tel { get; set; }

        public override string ToString() { 
            return "{" + ID +" "+ Name + " " + Age + " " + Six + " " + Address + " " + Tel + "}";
        }
    }
}
