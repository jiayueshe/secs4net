﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.Toolkit.HighPerformance;
using Microsoft.Toolkit.HighPerformance.Buffers;
using Secs4Net.Benchmark;
using Secs4Net.Extensions;
using System;
using System.Buffers;

namespace Secs4Net.Benchmarks;

[Config(typeof(BenchmarkConfig))]
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ReverseEndianness
{
    private IMemoryOwner<ushort> _uint16;
    private IMemoryOwner<uint> _uint32;
    private IMemoryOwner<ulong> _uint64;
    private IMemoryOwner<short> _int16;
    private IMemoryOwner<int> _int32;
    private IMemoryOwner<long> _int64;
    private IMemoryOwner<float> _single;
    private IMemoryOwner<double> _double;


    [Params(64)]
    public int Size { get; set; }

    [GlobalSetup]
    public unsafe void Setup()
    {
        ReverseEndiannessHelper<ushort>.Reverse(default);
        ReverseEndiannessHelper<uint>.Reverse(default);
        ReverseEndiannessHelper<ulong>.Reverse(default);
        ReverseEndiannessHelper<short>.Reverse(default);
        ReverseEndiannessHelper<int>.Reverse(default);
        ReverseEndiannessHelper<long>.Reverse(default);
        ReverseEndiannessHelper<float>.Reverse(default);
        ReverseEndiannessHelper<double>.Reverse(default);
        _uint16 = MemoryOwner<ushort>.Allocate(Size);
        _uint32 = MemoryOwner<uint>.Allocate(Size);
        _uint64 = MemoryOwner<ulong>.Allocate(Size);
        _int16 = MemoryOwner<short>.Allocate(Size);
        _int32 = MemoryOwner<int>.Allocate(Size);
        _int64 = MemoryOwner<long>.Allocate(Size);
        _single = MemoryOwner<float>.Allocate(Size);
        _double = MemoryOwner<double>.Allocate(Size);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _uint16.Dispose();
        _uint32.Dispose();
        _uint64.Dispose();
        _int16.Dispose();
        _int32.Dispose();
        _int64.Dispose();
        _single.Dispose();
        _double.Dispose();
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("UInt16")]
    public int UInt16_SliceAndReverse()
    {
        var bytes = _uint16.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(ushort))
        {
            bytes.Slice(i, sizeof(ushort)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("UInt16")]
    public unsafe int UInt16_ReverseEndiannessHelper()
    {
        var data = _uint16.Memory.Span;
        ReverseEndiannessHelper<ushort>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("UInt16")]
    public int UInt16_BinaryPrimitives()
    {
        var data = _uint16.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("UInt32")]
    public int UInt32_SliceAndReverse()
    {
        var bytes = _uint32.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(uint))
        {
            bytes.Slice(i, sizeof(uint)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("UInt32")]
    public unsafe int UInt32_ReverseEndiannessHelper()
    {
        var data = _uint32.Memory.Span;
        ReverseEndiannessHelper<uint>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("UInt32")]
    public int UInt32_BinaryPrimitives()
    {
        var data = _uint32.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("UInt64")]
    public int UInt64_SliceAndReverse()
    {
        var bytes = _uint64.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(ulong))
        {
            bytes.Slice(i, sizeof(ulong)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("UInt64")]
    public unsafe int UInt64_ReverseEndiannessHelper()
    {
        var data = _uint64.Memory.Span;
        ReverseEndiannessHelper<ulong>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("UInt64")]
    public int UInt64_BinaryPrimitives()
    {
        var data = _uint64.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("Int16")]
    public int Int16_SliceAndReverse()
    {
        var bytes = _int16.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(short))
        {
            bytes.Slice(i, sizeof(short)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("Int16")]
    public unsafe int Int16_ReverseEndiannessHelper()
    {
        var data = _int16.Memory.Span;
        ReverseEndiannessHelper<short>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("Int16")]
    public int Int16_BinaryPrimitives()
    {
        var data = _int16.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("Int32")]
    public int Int32_SliceAndReverse()
    {
        var bytes = _int32.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(int))
        {
            bytes.Slice(i, sizeof(int)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("Int32")]
    public unsafe int Int32_ReverseEndiannessHelper()
    {
        var data = _int32.Memory.Span;
        ReverseEndiannessHelper<int>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("Int32")]
    public int Int32_BinaryPrimitives()
    {
        var data = _int32.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("Int64")]
    public int Int64_SliceAndReverse()
    {
        var bytes = _int64.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(long))
        {
            bytes.Slice(i, sizeof(long)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("Int64")]
    public unsafe int Int64_ReverseEndiannessHelper()
    {
        var data = _int64.Memory.Span;
        ReverseEndiannessHelper<long>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("Int64")]
    public int Int64_BinaryPrimitives()
    {
        var data = _int64.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("Single")]
    public int Single_SliceAndReverse()
    {
        var bytes = _single.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(float))
        {
            bytes.Slice(i, sizeof(float)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("Single")]
    public unsafe int Single_ReverseEndiannessHelper()
    {
        var data = _single.Memory.Span;
        ReverseEndiannessHelper<float>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("Single")]
    public int Single_BinaryPrimitives()
    {
        var data = _single.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }

    [Benchmark(Description = "Slice & Reverse", Baseline = true)]
    [BenchmarkCategory("Double")]
    public int Double_SliceAndReverse()
    {
        var bytes = _double.Memory.Span.AsBytes();
        for (var i = 0; i < bytes.Length; i += sizeof(double))
        {
            bytes.Slice(i, sizeof(double)).Reverse();
        }
        return bytes.Length;
    }

    [Benchmark(Description = "ReverseEndiannessHelper")]
    [BenchmarkCategory("Double")]
    public unsafe int Double_ReverseEndiannessHelper()
    {
        var data = _double.Memory.Span;
        ReverseEndiannessHelper<double>.Reverse(data);
        return data.Length;
    }

    [Benchmark(Description = "BinaryPrimitives")]
    [BenchmarkCategory("Doulbe")]
    public int Double_BinaryPrimitives()
    {
        var data = _double.Memory.Span;
        data.ReverseEndianness();
        return data.Length;
    }
}
