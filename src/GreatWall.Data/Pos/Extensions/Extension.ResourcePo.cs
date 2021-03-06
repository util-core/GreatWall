﻿using GreatWall.Data.Pos.Models;
using GreatWall.Domain.Enums;
using GreatWall.Domain.Models;
using Util.Helpers;
using Util.Maps;

namespace GreatWall.Data.Pos.Extensions {
    /// <summary>
    /// 资源持久化对象扩展
    /// </summary>
    public static partial class Extension {
        /// <summary>
        /// 转换为模块
        /// </summary>
        public static Module ToModule( this ResourcePo po ) {
            if( po == null )
                return null;
            if ( po.Type != ResourceType.Module )
                return null;
            var result = po.MapTo( new Module( po.Id, po.Path, po.Level ) );
            result.Url = po.Uri;
            var extend = Json.ToObject<ModuleExtend>( po.Extend );
            extend.MapTo( result );
            return result;
        }

        /// <summary>
        /// 转换为资源持久化对象
        /// </summary>
        public static ResourcePo ToPo( this Module entity ) {
            if( entity == null )
                return null;
            var result = entity.MapTo<ResourcePo>();
            result.Type = ResourceType.Module;
            result.Uri = entity.Url;
            result.Extend = Json.ToJson( CreateExtend( entity ) );
            return result;
        }

        /// <summary>
        /// 创建模块扩展对象
        /// </summary>
        private static ModuleExtend CreateExtend( Module entity ) {
            return new ModuleExtend {
                Icon = entity.Icon,
                Expanded = entity.Expanded
            };
        }

        /// <summary>
        /// 转换为身份资源实体
        /// </summary>
        public static IdentityResource ToIdentityResource( this ResourcePo po ) {
            if( po == null )
                return null;
            var result = po.MapTo( new IdentityResource( po.Id ) );
            var extend = Json.ToObject<IdentityResourceExtend>( po.Extend );
            extend.MapTo( result );
            return result;
        }

        /// <summary>
        /// 转换为资源持久化对象
        /// </summary>
        public static ResourcePo ToPo( this IdentityResource entity ) {
            if( entity == null )
                return null;
            var result = entity.MapTo<ResourcePo>();
            result.Type = ResourceType.Identity;
            result.Extend = Json.ToJson( CreateExtend( entity ) );
            return result;
        }

        /// <summary>
        /// 创建身份资源扩展对象
        /// </summary>
        private static IdentityResourceExtend CreateExtend( IdentityResource entity ) {
            return entity.MapTo<IdentityResourceExtend>();
        }

        /// <summary>
        /// 转换为Api资源实体
        /// </summary>
        public static ApiResource ToApiResource( this ResourcePo po ) {
            if( po == null )
                return null;
            var result = po.MapTo( new ApiResource( po.Id ) );
            var extend = Json.ToObject<ApiResourceExtend>( po.Extend );
            extend.MapTo( result );
            return result;
        }

        /// <summary>
        /// 转换为资源持久化对象
        /// </summary>
        public static ResourcePo ToPo( this ApiResource entity ) {
            if( entity == null )
                return null;
            var result = entity.MapTo<ResourcePo>();
            result.Type = ResourceType.Api;
            result.Extend = Json.ToJson( CreateExtend( entity ) );
            return result;
        }

        /// <summary>
        /// 创建Api资源扩展对象
        /// </summary>
        private static ApiResourceExtend CreateExtend( ApiResource entity ) {
            return entity.MapTo<ApiResourceExtend>();
        }
    }
}
