// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LruCache.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// 何翔华
// Taf.Core.Utility
// LruCache.cs

namespace Taf.Core.Utility;

using System;

/// <summary>
///  LRU缓存,最近最少使用Least Frequently Used,
/// 当缓存已满时，淘汰掉最不经常使用的缓存项。
/// 每个缓存项都有一个使用频率计数器，每次访问缓存项时，该计数器就会加1。
/// 当需要淘汰缓存项时，会选择使用频率最低的缓存项进行淘汰。这种策略适用于访问频率较低的缓存项，可以避免频繁访问的缓存项被淘汰。
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class LruCache<TKey, TValue> where TKey : notnull{
    private readonly DoubleLinkedListNode<TKey, TValue> _head;

    private readonly DoubleLinkedListNode<TKey, TValue> _tail;

    private readonly Dictionary<TKey, DoubleLinkedListNode<TKey, TValue>> _dictionary;

    private readonly int    _capacity;
    private readonly object _locker = new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="capacity">默认缓存队列包含100条记录</param>
    public LruCache(int capacity=100)
    {
        _capacity = capacity;
        _head = new DoubleLinkedListNode<TKey, TValue>();
        _tail = new DoubleLinkedListNode<TKey, TValue>();
        _head.Next = _tail;
        _tail.Previous = _head;
        _dictionary = new Dictionary<TKey, DoubleLinkedListNode<TKey, TValue>>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="func">如果不在缓存中,添加到缓存</param>
    /// <returns></returns>
    public TValue? Get(TKey key, Func<TKey,TValue>? func=null)
    {
        lock(_locker){
            if (_dictionary.TryGetValue(key, out var node))
            {
                RemoveNode(node);
                AddLastNode(node);
                return node.Value;
            }
        }

        if (func!=null){
            var value= func(key);
            Put(key, value);
            return value;
        }
        return default;
    }
    
    public void Clear(){
        lock(_locker){
            _dictionary.Clear();
            _head.Next = _tail;
            _tail.Next = _head;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Put(TKey key, TValue value){
        lock(_locker){
            if(_dictionary.TryGetValue(key, out var node)){
                RemoveNode(node);
                AddLastNode(node);
                node.Value = value;
            } else{
                if(_dictionary.Count == _capacity){
                    var firstNode = RemoveFirstNode();

                    _dictionary.Remove(firstNode.Key);
                }

                var newNode = new DoubleLinkedListNode<TKey, TValue>(key, value);
                AddLastNode(newNode);
                _dictionary.TryAdd(key, newNode);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    public void Remove(TKey key){
        lock(_locker){
            if(_dictionary.TryGetValue(key, out var node)){
                _dictionary.Remove(key);
                RemoveNode(node);
            }
        }
    }

    private void AddLastNode(DoubleLinkedListNode<TKey, TValue> node)
    {
        node.Previous = _tail.Previous;
        node.Next = _tail;
        _tail.Previous.Next = node;
        _tail.Previous = node;
    }

    private DoubleLinkedListNode<TKey, TValue> RemoveFirstNode()
    {
        var firstNode = _head.Next;
        _head.Next = firstNode.Next;
        firstNode.Next.Previous = _head;
        firstNode.Next = null;
        firstNode.Previous = null;
        return firstNode;
    }

    private void RemoveNode(DoubleLinkedListNode<TKey, TValue> node)
    {
        node.Previous.Next = node.Next;
        node.Next.Previous = node.Previous;
        node.Next = null;
        node.Previous = null;
    }
    
    internal class DoubleLinkedListNode<TKey, TValue>
    {    
        public DoubleLinkedListNode()
        {
        }

        public DoubleLinkedListNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }
        
        public TValue Value { get; set; }

        public DoubleLinkedListNode<TKey, TValue> Previous { get; set; }

        public DoubleLinkedListNode<TKey, TValue> Next { get; set; }
    }
}
