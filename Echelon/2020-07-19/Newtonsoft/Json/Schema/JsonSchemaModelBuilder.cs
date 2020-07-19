using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C7 RID: 455
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModelBuilder
	{
		// Token: 0x060011C7 RID: 4551 RVA: 0x0006245C File Offset: 0x0006065C
		public JsonSchemaModel Build(JsonSchema schema)
		{
			this._nodes = new JsonSchemaNodeCollection();
			this._node = this.AddSchema(null, schema);
			this._nodeModels = new Dictionary<JsonSchemaNode, JsonSchemaModel>();
			return this.BuildNodeModel(this._node);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00062490 File Offset: 0x00060690
		public JsonSchemaNode AddSchema(JsonSchemaNode existingNode, JsonSchema schema)
		{
			string id;
			if (existingNode != null)
			{
				if (existingNode.Schemas.Contains(schema))
				{
					return existingNode;
				}
				id = JsonSchemaNode.GetId(existingNode.Schemas.Union(new JsonSchema[]
				{
					schema
				}));
			}
			else
			{
				id = JsonSchemaNode.GetId(new JsonSchema[]
				{
					schema
				});
			}
			if (this._nodes.Contains(id))
			{
				return this._nodes[id];
			}
			JsonSchemaNode jsonSchemaNode = (existingNode != null) ? existingNode.Combine(schema) : new JsonSchemaNode(schema);
			this._nodes.Add(jsonSchemaNode);
			this.AddProperties(schema.Properties, jsonSchemaNode.Properties);
			this.AddProperties(schema.PatternProperties, jsonSchemaNode.PatternProperties);
			if (schema.Items != null)
			{
				for (int i = 0; i < schema.Items.Count; i++)
				{
					this.AddItem(jsonSchemaNode, i, schema.Items[i]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				this.AddAdditionalItems(jsonSchemaNode, schema.AdditionalItems);
			}
			if (schema.AdditionalProperties != null)
			{
				this.AddAdditionalProperties(jsonSchemaNode, schema.AdditionalProperties);
			}
			if (schema.Extends != null)
			{
				foreach (JsonSchema schema2 in schema.Extends)
				{
					jsonSchemaNode = this.AddSchema(jsonSchemaNode, schema2);
				}
			}
			return jsonSchemaNode;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00062610 File Offset: 0x00060810
		public void AddProperties(IDictionary<string, JsonSchema> source, IDictionary<string, JsonSchemaNode> target)
		{
			if (source != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in source)
				{
					this.AddProperty(target, keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00062678 File Offset: 0x00060878
		public void AddProperty(IDictionary<string, JsonSchemaNode> target, string propertyName, JsonSchema schema)
		{
			JsonSchemaNode existingNode;
			target.TryGetValue(propertyName, out existingNode);
			target[propertyName] = this.AddSchema(existingNode, schema);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000626A4 File Offset: 0x000608A4
		public void AddItem(JsonSchemaNode parentNode, int index, JsonSchema schema)
		{
			JsonSchemaNode existingNode = (parentNode.Items.Count > index) ? parentNode.Items[index] : null;
			JsonSchemaNode jsonSchemaNode = this.AddSchema(existingNode, schema);
			if (parentNode.Items.Count <= index)
			{
				parentNode.Items.Add(jsonSchemaNode);
				return;
			}
			parentNode.Items[index] = jsonSchemaNode;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00062710 File Offset: 0x00060910
		public void AddAdditionalProperties(JsonSchemaNode parentNode, JsonSchema schema)
		{
			parentNode.AdditionalProperties = this.AddSchema(parentNode.AdditionalProperties, schema);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00062728 File Offset: 0x00060928
		public void AddAdditionalItems(JsonSchemaNode parentNode, JsonSchema schema)
		{
			parentNode.AdditionalItems = this.AddSchema(parentNode.AdditionalItems, schema);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00062740 File Offset: 0x00060940
		private JsonSchemaModel BuildNodeModel(JsonSchemaNode node)
		{
			JsonSchemaModel jsonSchemaModel;
			if (this._nodeModels.TryGetValue(node, out jsonSchemaModel))
			{
				return jsonSchemaModel;
			}
			jsonSchemaModel = JsonSchemaModel.Create(node.Schemas);
			this._nodeModels[node] = jsonSchemaModel;
			foreach (KeyValuePair<string, JsonSchemaNode> keyValuePair in node.Properties)
			{
				if (jsonSchemaModel.Properties == null)
				{
					jsonSchemaModel.Properties = new Dictionary<string, JsonSchemaModel>();
				}
				jsonSchemaModel.Properties[keyValuePair.Key] = this.BuildNodeModel(keyValuePair.Value);
			}
			foreach (KeyValuePair<string, JsonSchemaNode> keyValuePair2 in node.PatternProperties)
			{
				if (jsonSchemaModel.PatternProperties == null)
				{
					jsonSchemaModel.PatternProperties = new Dictionary<string, JsonSchemaModel>();
				}
				jsonSchemaModel.PatternProperties[keyValuePair2.Key] = this.BuildNodeModel(keyValuePair2.Value);
			}
			foreach (JsonSchemaNode node2 in node.Items)
			{
				if (jsonSchemaModel.Items == null)
				{
					jsonSchemaModel.Items = new List<JsonSchemaModel>();
				}
				jsonSchemaModel.Items.Add(this.BuildNodeModel(node2));
			}
			if (node.AdditionalProperties != null)
			{
				jsonSchemaModel.AdditionalProperties = this.BuildNodeModel(node.AdditionalProperties);
			}
			if (node.AdditionalItems != null)
			{
				jsonSchemaModel.AdditionalItems = this.BuildNodeModel(node.AdditionalItems);
			}
			return jsonSchemaModel;
		}

		// Token: 0x040008AE RID: 2222
		private JsonSchemaNodeCollection _nodes = new JsonSchemaNodeCollection();

		// Token: 0x040008AF RID: 2223
		private Dictionary<JsonSchemaNode, JsonSchemaModel> _nodeModels = new Dictionary<JsonSchemaNode, JsonSchemaModel>();

		// Token: 0x040008B0 RID: 2224
		private JsonSchemaNode _node;
	}
}
