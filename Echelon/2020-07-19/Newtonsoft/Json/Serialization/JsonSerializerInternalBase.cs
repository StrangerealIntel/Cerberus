using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001AF RID: 431
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class JsonSerializerInternalBase
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00057E50 File Offset: 0x00056050
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this.Serializer = serializer;
			this.TraceWriter = serializer.TraceWriter;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00057E78 File Offset: 0x00056078
		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), "A different value already has the Id '{0}'.", "A different Id has already been assigned for value '{0}'. This error may be caused by an object being reused multiple times during deserialization and can be fixed with the setting ObjectCreationHandling.Replace.");
				}
				return this._mappings;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00057EAC File Offset: 0x000560AC
		protected NullValueHandling ResolvedNullValueHandling([Nullable(2)] JsonObjectContract containerContract, JsonProperty property)
		{
			NullValueHandling? nullValueHandling = property.NullValueHandling;
			if (nullValueHandling != null)
			{
				return nullValueHandling.GetValueOrDefault();
			}
			NullValueHandling? nullValueHandling2 = (containerContract != null) ? containerContract.ItemNullValueHandling : null;
			if (nullValueHandling2 == null)
			{
				return this.Serializer._nullValueHandling;
			}
			return nullValueHandling2.GetValueOrDefault();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00057F14 File Offset: 0x00056114
		private ErrorContext GetErrorContext([Nullable(2)] object currentObject, [Nullable(2)] object member, string path, Exception error)
		{
			if (this._currentErrorContext == null)
			{
				this._currentErrorContext = new ErrorContext(currentObject, member, path, error);
			}
			if (this._currentErrorContext.Error != error)
			{
				throw new InvalidOperationException("Current error context error is different to requested error.");
			}
			return this._currentErrorContext;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00057F54 File Offset: 0x00056154
		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			this._currentErrorContext = null;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00057F74 File Offset: 0x00056174
		[NullableContext(2)]
		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, IJsonLineInfo lineInfo, [Nullable(1)] string path, [Nullable(1)] Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, path, ex);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Error && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = (base.GetType() == typeof(JsonSerializerInternalWriter)) ? "Error serializing" : "Error deserializing";
				if (contract != null)
				{
					string str = text;
					string str2 = " ";
					Type underlyingType = contract.UnderlyingType;
					text = str + str2 + ((underlyingType != null) ? underlyingType.ToString() : null);
				}
				text = text + ". " + ex.Message;
				if (!(ex is JsonException))
				{
					text = JsonPosition.FormatMessage(lineInfo, path, text);
				}
				this.TraceWriter.Trace(TraceLevel.Error, text, ex);
			}
			if (contract != null && currentObject != null)
			{
				contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		// Token: 0x0400081D RID: 2077
		[Nullable(2)]
		private ErrorContext _currentErrorContext;

		// Token: 0x0400081E RID: 2078
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private BidirectionalDictionary<string, object> _mappings;

		// Token: 0x0400081F RID: 2079
		internal readonly JsonSerializer Serializer;

		// Token: 0x04000820 RID: 2080
		[Nullable(2)]
		internal readonly ITraceWriter TraceWriter;

		// Token: 0x04000821 RID: 2081
		[Nullable(2)]
		protected JsonSerializerProxy InternalSerializer;

		// Token: 0x020002F4 RID: 756
		[Nullable(0)]
		private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x06001835 RID: 6197 RVA: 0x00079488 File Offset: 0x00077688
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06001836 RID: 6198 RVA: 0x00079490 File Offset: 0x00077690
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}
		}
	}
}
