using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HotChocolate.Types.Descriptors;

namespace CosmoResearch.GraphQL.Common {

    public class InheritanceAwareTypeInspector : DefaultTypeInspector
    {
        // https://github.com/ChilliCream/hotchocolate/issues/2019
        public override IEnumerable<MemberInfo> GetMembers(Type type)
        {
            return base.GetMembers(type)
                .Where(memberInfo => memberInfo.DeclaringType == type);
        }
    }

}
