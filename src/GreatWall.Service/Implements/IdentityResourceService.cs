﻿using System;
using System.Threading.Tasks;
using GreatWall.Data;
using GreatWall.Domain.Models;
using GreatWall.Domain.Repositories;
using GreatWall.Service.Abstractions;
using GreatWall.Service.Dtos;
using GreatWall.Service.Dtos.Extensions;
using Util;
using Util.Applications;
using Util.Exceptions;

namespace GreatWall.Service.Implements {
    /// <summary>
    /// 身份资源服务
    /// </summary>
    public class IdentityResourceService : ServiceBase, IIdentityResourceService {
        /// <summary>
        /// 初始化身份资源服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="identityResourceRepository">身份资源仓储</param>
        public IdentityResourceService( IGreatWallUnitOfWork unitOfWork, IIdentityResourceRepository identityResourceRepository ) {
            UnitOfWork = unitOfWork;
            IdentityResourceRepository = identityResourceRepository;
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IGreatWallUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// 身份资源仓储
        /// </summary>
        public IIdentityResourceRepository IdentityResourceRepository { get; set; }

        /// <summary>
        /// 创建身份资源
        /// </summary>
        /// <param name="dto">身份资源参数</param>
        public async Task<Guid> CreateAsync( IdentityResourceDto dto ) {
            var entity = dto.ToEntity();
            await ValidateCreateAsync( entity );
            entity.Init();
            await IdentityResourceRepository.AddAsync( entity );
            await UnitOfWork.CommitAsync();
            return entity.Id;
        }

        /// <summary>
        /// 验证创建身份资源
        /// </summary>
        private async Task ValidateCreateAsync( IdentityResource entity ) {
            entity.CheckNull( nameof( entity ) );
            if( await IdentityResourceRepository.CanCreateAsync( entity ) == false )
                ThrowUriRepeatException( entity );
        }

        /// <summary>
        /// 抛出资源标识重复异常
        /// </summary>
        private void ThrowUriRepeatException( IdentityResource identityResource ) {
            throw new Warning( string.Format( GreatWallResource.DuplicateUri, identityResource.Uri ) );
        }

        /// <summary>
        /// 修改身份资源
        /// </summary>
        /// <param name="dto">身份资源参数</param>
        public async Task UpdateAsync( IdentityResourceDto dto ) {
            var entity = dto.ToEntity();
            await ValidateUpdateAsync( entity );
            await IdentityResourceRepository.UpdateAsync( entity );
            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// 验证修改身份资源
        /// </summary>
        private async Task ValidateUpdateAsync( IdentityResource entity ) {
            entity.CheckNull( nameof( entity ) );
            if( await IdentityResourceRepository.CanUpdateAsync( entity ) == false )
                ThrowUriRepeatException( entity );
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        public async Task DeleteAsync( string ids ) {
            if( string.IsNullOrWhiteSpace( ids ) )
                return;
            var entities = await IdentityResourceRepository.FindByIdsAsync( ids );
            if( entities?.Count == 0 )
                return;
            await IdentityResourceRepository.RemoveAsync( entities );
            await UnitOfWork.CommitAsync();
        }
    }
}
