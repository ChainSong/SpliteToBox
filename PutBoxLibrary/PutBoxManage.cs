using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace PutBoxLibrary
{
    public abstract class PutBoxManage
    {
        private IEnumerable<BoxType> _boxlist;
        private IEnumerable<BoxInfo> _boxInfo;
    }
}
