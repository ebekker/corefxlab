﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;

namespace System.IO.Pipelines
{
    internal partial class Pipe
    {
        private struct Completion
        {
            private static readonly Exception _completedNoException = new Exception();

            private Exception _exception;

            public bool IsCompleted => _exception != null;

            public void TryComplete(Exception exception = null)
            {
                // Set the exception object to the exception passed in or a sentinel value
                Interlocked.CompareExchange(ref _exception, exception ?? _completedNoException, null);
            }

            public void ThrowIfFailed()
            {
                if (_exception != null && _exception != _completedNoException)
                {
                    throw _exception;
                }
            }
        }
    }
}
