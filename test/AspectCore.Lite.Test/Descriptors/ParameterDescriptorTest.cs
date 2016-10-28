﻿using AspectCore.Lite.Test.Fakes;
using AspectCore.Lite.Abstractions;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

#if NETCOREAPP1_0
using Microsoft.AspNetCore.Testing;
#endif

namespace AspectCore.Lite.Test.Abstractions.Descriptors
{
    public class ParameterDescriptorTest
    {
#if NETCOREAPP1_0
        [Fact]
        public void Ctor_ThrowsArgumentNullException()
        {
            ExceptionAssert.ThrowsArgumentNull(() => new ParameterDescriptor(null , null) , "parameterInfo");
        }

        [Theory]
        [InlineData("name" , 123)]
        [InlineData("name" , typeof(string))]
        public void Value_Set_ThrowInvalidOperationException0(string name , object value)
        {
            ParameterInfo info = MeaninglessService.Parameters[0];
            ParameterDescriptor descriptor = new ParameterDescriptor("LLL" , info);
            ExceptionAssert.Throws<InvalidOperationException>(() => { descriptor.Value = value; } , $"object type are not equal \"{name}\" parameter type or not a derived type of parameter type.");
        }

        [Theory]
        [InlineData("count" , "123")]
        [InlineData("count" , null)]
        public void Value_Set_ThrowInvalidOperationException1(string name , object value)
        {
            ParameterInfo info = MeaninglessService.Parameters[1];
            ParameterDescriptor descriptor = new ParameterDescriptor(1 , info);
            ExceptionAssert.Throws<InvalidOperationException>(() => { descriptor.Value = value; } , $"object type are not equal \"{name}\" parameter type or not a derived type of parameter type.");
        }

        [Theory]
        [InlineData("service" , "123")]
        public void Value_Set_ThrowInvalidOperationException2(string name , object value)
        {
            ParameterInfo info = MeaninglessService.Parameters[2];
            ParameterDescriptor descriptor = new ParameterDescriptor(null , info);
            ExceptionAssert.Throws<InvalidOperationException>(() => { descriptor.Value = value; } , $"object type are not equal \"{name}\" parameter type or not a derived type of parameter type.");
        }
#endif


        [Fact]
        public void Value_Set_ReferenceType()
        {
            ParameterInfo info = MeaninglessService.Parameters[2];
            ParameterDescriptor descriptor = new ParameterDescriptor(null , info);
            MeaninglessService service = new MeaninglessService();
            descriptor.Value = service;
            Assert.Equal(service , descriptor.Value);
        }

        [Fact]
        public void Value_Set_AssignableFrom_ReferenceType()
        {
            ParameterInfo info = MeaninglessService.Parameters[3];
            ParameterDescriptor descriptor = new ParameterDescriptor(null , info);
            MeaninglessService service = new MeaninglessService();
            descriptor.Value = service;
            Assert.Equal(service , descriptor.Value);
        }

        [Fact]
        public void Properties_Get()
        {
            ParameterInfo info = MeaninglessService.Parameters[0];
            ParameterDescriptor descriptor = new ParameterDescriptor("LLL" , info);

            Assert.NotNull(descriptor);
            Assert.Equal(descriptor.MetaDataInfo , info);
            Assert.Equal(descriptor.Name , info.Name);
            Assert.Equal(descriptor.Value , "LLL");
            Assert.Equal(descriptor.ParameterType , info.ParameterType);
            Assert.Equal(descriptor.CustomAttributes.Length , info.GetCustomAttributes().Count());
        }
    }
}
