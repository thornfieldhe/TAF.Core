// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkListExt.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Taf.Core.Utility{
    using System;

    /// <summary>
    /// 链表扩展类
    /// </summary>
    public static class LinkListExt{
         /// <summary>
         /// 交换链表的2个元素
         /// </summary>
         /// <param name="link"></param>
         /// <param name="from"></param>
         /// <param name="to"></param>
         /// <typeparam name="T"></typeparam>
         /// <returns></returns>
         /// <exception cref="ArgumentNullException"></exception>
         public static LinkedList<T> Switch<T>(this LinkedList<T> link, T from, T to){
            var f     = link.Find(from);
            var t     = link.Find(to);
            if (f==null)
            {  
                throw new ArgumentNullException($"链表中不存在交换对象[from]");
            }
            if (t==null)
            {  
                throw new ArgumentNullException($"链表中不存在交换对象[to]");
            }

            void NotAllowNull(LinkedListNode<T> itemA, LinkedListNode<T> itemB){
                if(itemA == null){
                    throw new ArgumentNullException($"链表中元素{f.Value}的后置元素不能为空");
                }

                if(itemB == null){
                    throw new ArgumentNullException($"链表中元素{t.Value}的前置元素不能为空");
                }
            }
            
            LinkedListNode<T> a;
            LinkedListNode<T> b;
            if (f.Next==t)
            {  
              link.Remove(f);  
              link.AddAfter(t,f);
              return link;
            }
            
            if (f.Previous==t)
            {  
                link.Remove(f);  
                link.AddBefore(t, f);
                return link; 
            }
            if (link.First ==f || link.Last ==t) // f在首情况||t在尾情况
            {
                a = f.Next;
                b = t.Previous;
                NotAllowNull(a, b);
                link.Remove(f);
                link.AddAfter(b,f);
                link.Remove(t);
                link.AddBefore(a,t);
                return link;
            }
            
            if (link.Last ==f || link.First ==t) // f在尾情况||t在首情况||在中间情况
            {  
                a = f.Previous;
                b = t.Next;
                NotAllowNull(a, b);
                link.Remove(f);
                link.AddBefore(b, f);
                link.Remove(t);
                link.AddAfter(a, t);
                return link; 
            }
            
            throw new  Exception("未定义此类情形");
        }
    }
}
