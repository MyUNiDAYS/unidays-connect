<template>
    <div class="upcoming_event">
        <div class="upcoming_event_wrapper">
            <div class="upcoming_event_image">
                <img
                    class="primary_img"
                    src="@/assets/images/events/large_1.jpg"
                    alt=""
                />
            </div>
            <div class="upcoming_event_content">
                <div class="row">
                    <div class="col-lg-8 offset-lg-2">
                        <div id="clockdiv">
                            <div class="fifth">
                                <span class="days">110</span>
                                <div class="smalltext">Days</div>
                            </div>
                            <div class="first">
                                <span class="hours">21</span>
                                <div class="smalltext">Hours</div>
                            </div>
                            <div class="second">
                                <span class="minutes">32</span>
                                <div class="smalltext">Minutes</div>
                            </div>
                            <div class="third">
                                <span class="seconds">11</span>
                                <div class="smalltext">Seconds</div>
                            </div>
                        </div>
                        <div class="upcoming_event_content_inner">
                            <div class="upcoming_event_content_left">
                                <div class="highlight_tag">Latest Event</div>
                                <h3>
                                    <a href="#">{{ eventData.title }}</a>
                                </h3>
                                <h6 class="upcoming_event_location">
                                    <i class="ion-ios-pin"></i> Albert Hall
                                    Conference Centre, N Circus St, Nottingham
                                    NG1 5AA
                                </h6>
                            </div>
                            <div class="upcoming_event_content_right">
                                <div class="power_button_group">
                                    <router-link
                                        :to="{
                                            name: 'EventSignup',
                                            params: { id: eventData.id }
                                        }"
                                        class="power_button effect_1"
                                    >
                                        <span class="button_value"
                                            >Register Now!</span
                                        >
                                    </router-link>
                                </div>
                                <p>
                                    <span>10</span> seats left only. So hurry !!
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: {
        eventData: {
            type: String,
            required: true
        }
    }
};
</script>
<style lang="sass" scoped>
.button_value
  white-space: nowrap
.upcoming_event_content_right
  p
    text-align: center
</style>
