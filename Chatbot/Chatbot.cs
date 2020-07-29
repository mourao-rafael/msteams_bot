// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EmptyBot .NET Template version v4.9.1

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Chatbot
{
    public class EmptyBot : ActivityHandler
    {
        private static string[] comandos = {"oi", "help"};

        // Método executado quando um novo usuário inicia uma conversa:
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id){
                    // await turnContext.SendActivityAsync(MessageFactory.Text($"Hello world!"), cancellationToken);
                    await turnContext.SendActivityAsync($"Olá, " + turnContext.Activity.From.Name + "!");
                    await HelpMsg(turnContext, cancellationToken);
                }
            }
        }

        // Método executado sempre que receber uma nova mensagem:
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken){
            if(turnContext.Activity.Text.Equals(comandos[0])) await turnContext.SendActivityAsync($"Olá, " + turnContext.Activity.From.Name + "!");
            else if(turnContext.Activity.Text.Equals(comandos[1])) await HelpMsg(turnContext, cancellationToken);

            else{ // Caso o comando não seja entendido:
                await turnContext.SendActivityAsync("Desculpe, não entendo este comando. Digite \"help\" para ver os comandos que eu entendo!");
            }
        }

        // Método para listar os comandos que o bot entende, através de botões:
        private static async Task HelpMsg(ITurnContext turnContext, CancellationToken cancellationToken){
            var helpMsg = MessageFactory.Text("Aqui está a lista de comandos que eu entendo:");

            // Converter lista de comandos em botões:
            List<CardAction> card = new List<CardAction>();
            foreach(string cmd in comandos){
                card.Add(new CardAction() { Title=cmd, Type=ActionTypes.ImBack, Value=cmd });
            }
            // Vincular botões à mensagem a ser enviada:
            helpMsg.SuggestedActions = new SuggestedActions(){ 
                Actions = card
            };
            
            await turnContext.SendActivityAsync(helpMsg, cancellationToken); // envia o card criado
        }
    }
}
