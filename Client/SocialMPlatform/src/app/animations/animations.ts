import {trigger, style, transition, animate} from "@angular/animations";

export const fadeInAnimation =
   trigger('fadeIn', [
      // transition(":enter", [
      //    style({opacity: 0}),
      //    animate('500ms', style({opacity: 1}))
      // ]),
      transition(":leave",[
        style({opacity:1}),
        animate('500ms',style({opacity:0}))
      ])
   ]);
