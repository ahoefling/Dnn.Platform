(function(a){a.fn.dnnControlPanel=function(c){var d=a.extend({},a.fn.dnnControlPanel.defaultOptions,c),b=this;return b.each(function(){var i=a(this);i.wrap(function(){return d.wrappingHtml});if(a('[id$="CommonTasksPanel"]').length>0){a('[id$="CommonTasksPanel"]').detach().appendTo("#dnnCommonTasks .megaborder")}else{a("#dnnCommonTasks").remove()}a('[id$="CurrentPagePanel"]').detach().appendTo("#dnnCurrentPage .megaborder");if(a('[id$="AdminPanel"]').length>0){a('[id$="AdminPanel"]').detach().appendTo("#dnnOtherTools .megaborder")}else{a("#dnnOtherTools").remove()}var h=a("#dnnCPWrap");if(h.hasClass("Pinned")){h.parent().css({paddingBottom:"0"})}else{a(document.body).css({marginTop:h.outerHeight()})}a.fn.dnnControlPanel.show=function(){i.parents(d.controlPanelWrapper).slideDown()};a.fn.dnnControlPanel.hide=function(){i.parents(d.controlPanelWrapper).slideUp()};var l=false,k=false,i=a(this);function m(){if(!k){l=true}}function j(){g();var o=a(this).parent().find(d.menuBorderSelector);if(!a.support.cssFloat&&o.prev("iframe").length==0){var n=a('<iframe frameborder="0"></iframe>');o.before(n);n.css({position:"absolute",width:o.outerWidth()+"px",height:o.outerHeight()+"px",left:o.position().left+"px",top:o.position().top+"px",opacity:0})}o.stop().fadeTo("fast",1).show()}function g(){i.find(d.menuBorderSelector).stop().fadeTo("fast",0,function(){if(!a.support.cssFloat){a(this).prev("iframe").remove()}a(this).hide()})}var f={sensitivity:2,interval:200,over:j,timeout:100,out:function(){return null}};var e={sensitivity:2,interval:1,over:function(){return},timeout:300,out:function(){if(l){g()}}};i.find(d.menuBorderSelector).css({opacity:"0"});i.find(d.menuBorderSelector).find(d.primaryActionSelector).css({opacity:1});i.find(d.menuItemSelector).mouseenter(m);a(document).dblclick(function(){if(!l){g()}});i.dblclick(function(n){n.stopPropagation()});a(document).keyup(function(n){if(n.keyCode==27){g()}});i.find(d.menuItemActionSelector).hoverIntent(f);i.find(d.menuItemSelector).hoverIntent(e);i.find(d.enableAutohideOnFocusSelector).focus(function(){l=true});i.on("mouseover",d.persistOnMouseoverSelector,function(){l=false});i.on("click",d.persistOnWaitForChangeSelector,function(){k=true;l=false;setTimeout(function(){a(document).one("click",function(){k=false;l=true})},0)});i.on("focus",d.persistOnFocusSelector,function(){l=false})})};a.fn.dnnControlPanel.defaultOptions={wrappingHtml:'<div id="dnnCPWrap"><div id="dnnCPWrapLeft"><div id="dnnCPWrapRight"></div></div></div>',persistOnFocusSelector:"select, input",persistOnMouseoverSelector:".rcbSlide li",persistOnWaitForChangeSelector:".RadComboBox",enableAutohideOnFocusSelector:".dnnadminmega .megaborder",menuItemActionSelector:".dnnadminmega > li > a",menuItemSelector:".dnnadminmega > li",menuBorderSelector:".megaborder",primaryActionSelector:".dnnPrimaryAction",controlPanelWrapper:"#dnnCPWrap"};a(document).ready(function(){a(".dnnControlPanel").dnnControlPanel()})})(jQuery);
