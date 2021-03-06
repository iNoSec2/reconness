﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReconNess.Core;
using ReconNess.Core.Models;
using ReconNess.Core.Services;
using ReconNess.Entities;

namespace ReconNess.Services
{
    /// <summary>
    /// This class implement <see cref="ISubdomainService"/> 
    /// </summary>
    public class SubdomainService : Service<Subdomain>, IService<Subdomain>, ISubdomainService
    {
        private readonly IServiceService serviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ISubdomainService" /> class
        /// </summary>
        /// <param name="unitOfWork"><see cref="IUnitOfWork"/></param>
        /// <param name="serviceService"><see cref="IServiceService"/></param>
        public SubdomainService(
            IUnitOfWork unitOfWork,
            IServiceService serviceService)
            : base(unitOfWork)
        {
            this.serviceService = serviceService;
        }

        /// <summary>
        /// <see cref="ISubdomainService.UpdateSubdomainAsync(Subdomain, Agent, ScriptOutput, bool, CancellationToken)"/>
        /// </summary>
        public async Task UpdateSubdomainAsync(Subdomain subdomain, Agent agent, ScriptOutput scriptOutput, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            (await 
                this.UpdateSubdomainIpAddress(subdomain, scriptOutput)
                    .UpdateSubdomainIsAlive(subdomain, scriptOutput)
                    .UpdateSubdomainHasHttpOpen(subdomain, scriptOutput)
                    .UpdateSubdomainTakeover(subdomain, scriptOutput)
                    .UpdateSubdomainServiceAsync(subdomain, scriptOutput, cancellationToken)
            ).UpdateSubdomainAgent(subdomain, agent);
            
            this.UnitOfWork.Repository<Subdomain>().Update(subdomain, cancellationToken);
        }

        /// <summary>
        /// <see cref="ISubdomainService.DeleteSubdomains(ICollection{Subdomain}, CancellationToken)"/>
        /// </summary>
        public void DeleteSubdomains(ICollection<Subdomain> subdomains, CancellationToken cancellationToken = default)
        {
            foreach (var subdomain in subdomains)
            {
                cancellationToken.ThrowIfCancellationRequested();

                this.UnitOfWork.Repository<Service>().DeleteRange(subdomain.Services.ToList(), cancellationToken);
                this.UnitOfWork.Repository<Subdomain>().Delete(subdomain, cancellationToken);
            }
        }

        /// <summary>
        /// Assign Ip address to the subdomain
        /// </summary>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="scriptOutput">The terminal output one line</param>
        /// <returns><see cref="ISubdomainService"/></returns>
        private SubdomainService UpdateSubdomainIpAddress(Subdomain subdomain, ScriptOutput scriptOutput)
        {
            if (scriptOutput.Ip != null && Helpers.Helpers.ValidateIPv4(scriptOutput.Ip) && subdomain.IpAddress != scriptOutput.Ip)
            {
                subdomain.IpAddress = scriptOutput.Ip;
            }

            return this;
        }

        /// <summary>
        /// Update the subdomain if it has http port open
        /// </summary>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="scriptOutput">The terminal output one line</param>
        /// <returns><see cref="ISubdomainService"/></returns>
        private SubdomainService UpdateSubdomainHasHttpOpen(Subdomain subdomain, ScriptOutput scriptOutput)
        {
            if (scriptOutput.HasHttpOpen != null && subdomain.HasHttpOpen != scriptOutput.HasHttpOpen.Value)
            {
                subdomain.HasHttpOpen = scriptOutput.HasHttpOpen.Value;
            }

            return this;
        }

        /// <summary>
        /// Update the subdomain if it can be takeover
        /// </summary>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="scriptOutput">The terminal output one line</param>
        /// <returns><see cref="ISubdomainService"/></returns>
        private SubdomainService UpdateSubdomainTakeover(Subdomain subdomain, ScriptOutput scriptOutput)
        {
            if (scriptOutput.Takeover != null && subdomain.Takeover != scriptOutput.Takeover.Value)
            {
                subdomain.Takeover = scriptOutput.Takeover.Value;
            }

            return this;
        }

        /// <summary>
        /// Update the subdomain if is a new service with open port
        /// </summary>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="scriptOutput">The terminal output one line</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="ISubdomainService"/></returns>
        private async Task<SubdomainService> UpdateSubdomainServiceAsync(Subdomain subdomain, ScriptOutput scriptOutput, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(scriptOutput.Service))
            {
                return this;
            }

            var service = new Service
            {
                Name = scriptOutput.Service,
                Port = scriptOutput.Port.Value
            };

            if (!(await this.serviceService.IsAssignedToSubdomainAsync(subdomain, service, cancellationToken)))
            {
                if (subdomain.Services == null)
                {
                    subdomain.Services = new List<Service>();
                }

                subdomain.Services.Add(service);
            }

            return this;
        }

        /// <summary>
        /// Update the subdomain if is Alive
        /// </summary>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="scriptOutput">The terminal output one line</param>
        /// <returns><see cref="ISubdomainService"/></returns>
        private SubdomainService UpdateSubdomainIsAlive(Subdomain subdomain, ScriptOutput scriptOutput)
        {
            if (scriptOutput.IsAlive != null && subdomain.IsAlive != scriptOutput.IsAlive)
            {
                subdomain.IsAlive = scriptOutput.IsAlive.Value;
            }

            return this;
        }

        /// <summary>
        /// Update the subdomain agent property with the agent name if was updated before
        /// </summary>
        /// <param name="subdomain">The subdomain</param>
        /// <param name="agent">The agent</param>
        private void UpdateSubdomainAgent(Subdomain subdomain, Agent agent)
        {
            if (string.IsNullOrWhiteSpace(subdomain.FromAgents))
            {
                subdomain.FromAgents = agent.Name;
            }
            else if (!subdomain.FromAgents.Contains(agent.Name))
            {
                subdomain.FromAgents = string.Join(", ", subdomain.FromAgents, agent.Name);
            }
        }
    }
}
