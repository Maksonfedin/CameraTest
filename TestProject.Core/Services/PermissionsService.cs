﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TestProject.Core.Services.Interfaces;

namespace TestProject.Core.Services
{
	public class PermissionsService : IPermissionsService
	{
		public IPermissions Instance => CrossPermissions.Current;

		public async Task<bool> CheckPermissionsAccesGrantedAsync(List<Permission> permissions)
		{
			foreach (var item in permissions)
			{
				var res = await CheckPermissionsAccesGrantedAsync(item);

				if (!res)
					return false;
			}

			return true;
		}

		public bool OpenSettings()
		{
			return Instance.OpenAppSettings();
		}

		public async Task<bool> PreCheckPermissionsAccesGrantedAsync(Permission permission)
		{
			//PermissionStatus.
			return await CrossPermissions.Current.CheckPermissionStatusAsync(permission) == PermissionStatus.Granted;
		}

		public async Task<PermissionStatus> PreCheckPermissionsAccessAsync(Permission permission)
		{
			var result = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
			return result;
		}

		public async Task<bool> CheckPermissionsAccesGrantedAsync(Permission permission)
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

				if (status != PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
					{
						// TODO: Ask for Permission
					}
					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { permission });
					status = results[permission];
				}
				if (status == PermissionStatus.Granted)
				{
					return true;
				}
				else if (status != PermissionStatus.Unknown)
				{
					return false;
				}
			}
			catch (Exception e)
			{

			}
			return false;
		}
	}
}
