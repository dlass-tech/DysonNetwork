using System;
using System.Collections.Generic;
using System.Text.Json;
using DysonNetwork.Shared.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace DysonNetwork.Sphere.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activity_pub_deliveries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_id = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    activity_type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    inbox_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    actor_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    retry_count = table.Column<int>(type: "integer", nullable: false),
                    error_message = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    last_attempt_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    next_retry_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    sent_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    response_status_code = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    activity_payload = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activity_pub_deliveries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "automod_rules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    default_action = table.Column<int>(type: "integer", nullable: false),
                    pattern = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    is_regex = table.Column<bool>(type: "boolean", nullable: false),
                    derank_weight = table.Column<int>(type: "integer", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    expires_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_automod_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "delivery_dead_letters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inbox_uri = table.Column<string>(type: "text", nullable: false),
                    actor_uri = table.Column<string>(type: "text", nullable: false),
                    activity_type = table.Column<string>(type: "text", nullable: false),
                    activity_payload = table.Column<string>(type: "text", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    retry_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    failed_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_dead_letters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discovery_preferences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kind = table.Column<int>(type: "integer", nullable: false),
                    reference_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    applied_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discovery_preferences", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fediverse_instances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    domain = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    software = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    version = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    icon_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    thumbnail_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    contact_email = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    contact_account_username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    active_users = table.Column<int>(type: "integer", nullable: true),
                    metadata = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true),
                    is_blocked = table.Column<bool>(type: "boolean", nullable: false),
                    is_silenced = table.Column<bool>(type: "boolean", nullable: false),
                    block_reason = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    last_fetched_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    last_activity_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    metadata_fetched_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fediverse_instances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fediverse_moderation_rules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    domain = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    keyword_pattern = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    is_regex = table.Column<bool>(type: "boolean", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    report_threshold = table.Column<int>(type: "integer", nullable: true),
                    expires_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    is_system_rule = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fediverse_moderation_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post_interest_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kind = table.Column<int>(type: "integer", nullable: false),
                    reference_id = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<double>(type: "double precision", nullable: false),
                    interaction_count = table.Column<int>(type: "integer", nullable: false),
                    last_interacted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    last_signal_type = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_interest_profiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post_tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    nick = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    bio = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    picture = table.Column<SnCloudFileReferenceObject>(type: "jsonb", nullable: true),
                    background = table.Column<SnCloudFileReferenceObject>(type: "jsonb", nullable: true),
                    verification = table.Column<SnVerificationMark>(type: "jsonb", nullable: true),
                    meta = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true),
                    account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    realm_id = table.Column<Guid>(type: "uuid", nullable: true),
                    shadowban_reason = table.Column<int>(type: "integer", nullable: true),
                    shadowbanned_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    gatekept_follows = table.Column<bool>(type: "boolean", nullable: true),
                    moderate_subscription = table.Column<bool>(type: "boolean", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publishers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fediverse_actors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    display_name = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    bio = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    inbox_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    outbox_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    followers_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    following_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    featured_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    public_key_id = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    public_key = table.Column<string>(type: "character varying(8192)", maxLength: 8192, nullable: true),
                    metadata = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true),
                    avatar_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    header_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    is_bot = table.Column<bool>(type: "boolean", nullable: false),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    is_discoverable = table.Column<bool>(type: "boolean", nullable: false),
                    is_community = table.Column<bool>(type: "boolean", nullable: false),
                    realm_id = table.Column<Guid>(type: "uuid", nullable: true),
                    instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_fetched_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    last_activity_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    outbox_fetched_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fediverse_actors", x => x.id);
                    table.ForeignKey(
                        name: "fk_fediverse_actors_fediverse_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "fediverse_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_category_subscriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tag_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_category_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_category_subscriptions_post_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "post_categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_post_category_subscriptions_post_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "post_tags",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "live_streams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    visibility = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    room_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ingress_id = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ingress_stream_key = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    egress_id = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    hls_egress_id = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    hls_playlist_path = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    hls_started_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    started_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    ended_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    total_duration_seconds = table.Column<long>(type: "bigint", nullable: false),
                    viewer_count = table.Column<int>(type: "integer", nullable: false),
                    peak_viewer_count = table.Column<int>(type: "integer", nullable: false),
                    total_award_score = table.Column<decimal>(type: "numeric", nullable: false),
                    distributed_award_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    thumbnail = table.Column<SnCloudFileReferenceObject>(type: "jsonb", nullable: true),
                    metadata = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_live_streams", x => x.id);
                    table.ForeignKey(
                        name: "fk_live_streams_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "polls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    ended_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    is_anonymous = table.Column<bool>(type: "boolean", nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_polls", x => x.id);
                    table.ForeignKey(
                        name: "fk_polls_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_collections", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_collections_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publisher_features",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    flag = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    expired_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher_features", x => x.id);
                    table.ForeignKey(
                        name: "fk_publisher_features_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publisher_follow_requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    reviewed_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    reviewed_by_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    reject_reason = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher_follow_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_publisher_follow_requests_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publisher_members",
                columns: table => new
                {
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    joined_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher_members", x => new { x.publisher_id, x.account_id });
                    table.ForeignKey(
                        name: "fk_publisher_members_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publisher_subscriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_read_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    notify = table.Column<bool>(type: "boolean", nullable: false),
                    ended_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    end_reason = table.Column<int>(type: "integer", nullable: true),
                    ended_by_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fk_publisher_subscriptions_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publishing_settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    default_posting_publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    default_reply_publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    default_fediverse_publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publishing_settings", x => x.id);
                    table.ForeignKey(
                        name: "fk_publishing_settings_publishers_default_fediverse_publisher_",
                        column: x => x.default_fediverse_publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_publishing_settings_publishers_default_posting_publisher_id",
                        column: x => x.default_posting_publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_publishing_settings_publishers_default_reply_publisher_id",
                        column: x => x.default_reply_publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "sticker_packs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    icon = table.Column<SnCloudFileReferenceObject>(type: "jsonb", nullable: true),
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    prefix = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sticker_packs", x => x.id);
                    table.ForeignKey(
                        name: "fk_sticker_packs_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fediverse_keys",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key_id = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    key_pem = table.Column<string>(type: "TEXT", nullable: false),
                    private_key_pem = table.Column<string>(type: "TEXT", nullable: true),
                    algorithm = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    rotated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fediverse_keys", x => x.id);
                    table.ForeignKey(
                        name: "fk_fediverse_keys_fediverse_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "fediverse_relationships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target_actor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    is_muting = table.Column<bool>(type: "boolean", nullable: false),
                    is_blocking = table.Column<bool>(type: "boolean", nullable: false),
                    followed_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    reject_reason = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    realm_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fediverse_relationships", x => x.id);
                    table.ForeignKey(
                        name: "fk_fediverse_relationships_fediverse_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fediverse_relationships_fediverse_actors_target_actor_id",
                        column: x => x.target_actor_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "live_stream_awards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    attitude = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    live_stream_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_live_stream_awards", x => x.id);
                    table.ForeignKey(
                        name: "fk_live_stream_awards_live_streams_live_stream_id",
                        column: x => x.live_stream_id,
                        principalTable: "live_streams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "live_stream_chat_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    live_stream_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    content = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    timeout_until = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_live_stream_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_live_stream_chat_messages_live_streams_live_stream_id",
                        column: x => x.live_stream_id,
                        principalTable: "live_streams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "poll_answers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    answer = table.Column<Dictionary<string, JsonElement>>(type: "jsonb", nullable: true),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    poll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_poll_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_poll_answers_polls_poll_id",
                        column: x => x.poll_id,
                        principalTable: "polls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "poll_questions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    options = table.Column<List<SnPollOption>>(type: "jsonb", nullable: true),
                    title = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    poll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_poll_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_poll_questions_polls_poll_id",
                        column: x => x.poll_id,
                        principalTable: "polls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sticker_pack_ownerships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    pack_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sticker_pack_ownerships", x => x.id);
                    table.ForeignKey(
                        name: "fk_sticker_pack_ownerships_sticker_packs_pack_id",
                        column: x => x.pack_id,
                        principalTable: "sticker_packs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stickers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    image = table.Column<SnCloudFileReferenceObject>(type: "jsonb", nullable: false),
                    pack_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stickers", x => x.id);
                    table.ForeignKey(
                        name: "fk_stickers_sticker_packs_pack_id",
                        column: x => x.pack_id,
                        principalTable: "sticker_packs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boosts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_pub_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    web_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    boosted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boosts", x => x.id);
                    table.ForeignKey(
                        name: "fk_boosts_fediverse_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_awards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    attitude = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_awards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post_category_links",
                columns: table => new
                {
                    categories_id = table.Column<Guid>(type: "uuid", nullable: false),
                    posts_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_category_links", x => new { x.categories_id, x.posts_id });
                    table.ForeignKey(
                        name: "fk_post_category_links_post_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "post_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_collection_links",
                columns: table => new
                {
                    collections_id = table.Column<Guid>(type: "uuid", nullable: false),
                    posts_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_collection_links", x => new { x.collections_id, x.posts_id });
                    table.ForeignKey(
                        name: "fk_post_collection_links_post_collections_collections_id",
                        column: x => x.collections_id,
                        principalTable: "post_collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_featured_records",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    featured_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    social_credits = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_featured_records", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post_reactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    attitude = table.Column<int>(type: "integer", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fediverse_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_local = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_reactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_reactions_fediverse_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "post_tag_links",
                columns: table => new
                {
                    posts_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_tag_links", x => new { x.posts_id, x.tags_id });
                    table.ForeignKey(
                        name: "fk_post_tag_links_post_tags_tags_id",
                        column: x => x.tags_id,
                        principalTable: "post_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    slug = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    edited_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    drafted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    published_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    visibility = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    pin_mode = table.Column<int>(type: "integer", nullable: true),
                    metadata = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: true),
                    sensitive_marks = table.Column<string>(type: "jsonb", nullable: true),
                    embed_view = table.Column<PostEmbedView>(type: "jsonb", nullable: true),
                    fediverse_uri = table.Column<string>(type: "character varying(8192)", maxLength: 8192, nullable: true),
                    fediverse_type = table.Column<int>(type: "integer", nullable: true),
                    language = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    mentions = table.Column<List<ContentMention>>(type: "jsonb", nullable: true),
                    boost_count = table.Column<int>(type: "integer", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: true),
                    views_unique = table.Column<int>(type: "integer", nullable: false),
                    views_total = table.Column<int>(type: "integer", nullable: false),
                    upvotes = table.Column<int>(type: "integer", nullable: false),
                    downvotes = table.Column<int>(type: "integer", nullable: false),
                    awarded_score = table.Column<decimal>(type: "numeric", nullable: false),
                    replies_count = table.Column<int>(type: "integer", nullable: false),
                    reaction_score = table.Column<int>(type: "integer", nullable: false),
                    replied_gone = table.Column<bool>(type: "boolean", nullable: false),
                    forwarded_gone = table.Column<bool>(type: "boolean", nullable: false),
                    replied_post_id = table.Column<Guid>(type: "uuid", nullable: true),
                    forwarded_post_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quote_authorization_id = table.Column<Guid>(type: "uuid", nullable: true),
                    realm_id = table.Column<Guid>(type: "uuid", nullable: true),
                    attachments = table.Column<List<SnCloudFileReferenceObject>>(type: "jsonb", nullable: false),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: true),
                    shadowban_reason = table.Column<int>(type: "integer", nullable: true),
                    shadowbanned_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    locked_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_posts_fediverse_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_posts_posts_forwarded_post_id",
                        column: x => x.forwarded_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_posts_replied_post_id",
                        column: x => x.replied_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "quote_authorizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fediverse_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    interacting_object_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    interaction_target_uri = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    target_post_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quote_post_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_valid = table.Column<bool>(type: "boolean", nullable: false),
                    revoked_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quote_authorizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_quote_authorizations_fediverse_actors_author_id",
                        column: x => x.author_id,
                        principalTable: "fediverse_actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quote_authorizations_posts_quote_post_id",
                        column: x => x.quote_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_quote_authorizations_posts_target_post_id",
                        column: x => x.target_post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_automod_rules_name_deleted_at",
                table: "automod_rules",
                columns: new[] { "name", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_boosts_actor_id",
                table: "boosts",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_boosts_post_id",
                table: "boosts",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_discovery_preferences_account_id_kind_reference_id_deleted_",
                table: "discovery_preferences",
                columns: new[] { "account_id", "kind", "reference_id", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_actors_instance_id",
                table: "fediverse_actors",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_actors_uri_deleted_at",
                table: "fediverse_actors",
                columns: new[] { "uri", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_instances_domain_deleted_at",
                table: "fediverse_instances",
                columns: new[] { "domain", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_keys_actor_id",
                table: "fediverse_keys",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_keys_key_id_deleted_at",
                table: "fediverse_keys",
                columns: new[] { "key_id", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_moderation_rules_domain",
                table: "fediverse_moderation_rules",
                column: "domain");

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_relationships_actor_id",
                table: "fediverse_relationships",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_fediverse_relationships_target_actor_id",
                table: "fediverse_relationships",
                column: "target_actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_live_stream_awards_live_stream_id",
                table: "live_stream_awards",
                column: "live_stream_id");

            migrationBuilder.CreateIndex(
                name: "ix_live_stream_chat_messages_live_stream_id",
                table: "live_stream_chat_messages",
                column: "live_stream_id");

            migrationBuilder.CreateIndex(
                name: "ix_live_streams_publisher_id",
                table: "live_streams",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_poll_answers_poll_id",
                table: "poll_answers",
                column: "poll_id");

            migrationBuilder.CreateIndex(
                name: "ix_poll_questions_poll_id",
                table: "poll_questions",
                column: "poll_id");

            migrationBuilder.CreateIndex(
                name: "ix_polls_publisher_id",
                table: "polls",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_awards_post_id",
                table: "post_awards",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_category_links_posts_id",
                table: "post_category_links",
                column: "posts_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_category_subscriptions_category_id",
                table: "post_category_subscriptions",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_category_subscriptions_tag_id",
                table: "post_category_subscriptions",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_collection_links_posts_id",
                table: "post_collection_links",
                column: "posts_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_collections_publisher_id",
                table: "post_collections",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_featured_records_post_id",
                table: "post_featured_records",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_interest_profiles_account_id_kind_reference_id_deleted",
                table: "post_interest_profiles",
                columns: new[] { "account_id", "kind", "reference_id", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_reactions_actor_id",
                table: "post_reactions",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_reactions_post_id",
                table: "post_reactions",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_tag_links_tags_id",
                table: "post_tag_links",
                column: "tags_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_actor_id",
                table: "posts",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_forwarded_post_id",
                table: "posts",
                column: "forwarded_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_publisher_id",
                table: "posts",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_quote_authorization_id",
                table: "posts",
                column: "quote_authorization_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_replied_post_id",
                table: "posts",
                column: "replied_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_publisher_features_publisher_id",
                table: "publisher_features",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_publisher_follow_requests_publisher_id",
                table: "publisher_follow_requests",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_publisher_subscriptions_publisher_id",
                table: "publisher_subscriptions",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_publishers_name_deleted_at",
                table: "publishers",
                columns: new[] { "name", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_publishing_settings_account_id_deleted_at",
                table: "publishing_settings",
                columns: new[] { "account_id", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_publishing_settings_default_fediverse_publisher_id",
                table: "publishing_settings",
                column: "default_fediverse_publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_publishing_settings_default_posting_publisher_id",
                table: "publishing_settings",
                column: "default_posting_publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_publishing_settings_default_reply_publisher_id",
                table: "publishing_settings",
                column: "default_reply_publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_quote_authorizations_author_id",
                table: "quote_authorizations",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_quote_authorizations_quote_post_id",
                table: "quote_authorizations",
                column: "quote_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_quote_authorizations_target_post_id",
                table: "quote_authorizations",
                column: "target_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_sticker_pack_ownerships_pack_id",
                table: "sticker_pack_ownerships",
                column: "pack_id");

            migrationBuilder.CreateIndex(
                name: "ix_sticker_packs_prefix_deleted_at",
                table: "sticker_packs",
                columns: new[] { "prefix", "deleted_at" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sticker_packs_publisher_id",
                table: "sticker_packs",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_stickers_pack_id",
                table: "stickers",
                column: "pack_id");

            migrationBuilder.CreateIndex(
                name: "ix_stickers_slug",
                table: "stickers",
                column: "slug");

            migrationBuilder.AddForeignKey(
                name: "fk_boosts_posts_post_id",
                table: "boosts",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_awards_posts_post_id",
                table: "post_awards",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_category_links_posts_posts_id",
                table: "post_category_links",
                column: "posts_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_collection_links_posts_posts_id",
                table: "post_collection_links",
                column: "posts_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_featured_records_posts_post_id",
                table: "post_featured_records",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_reactions_posts_post_id",
                table: "post_reactions",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_tag_links_posts_posts_id",
                table: "post_tag_links",
                column: "posts_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_posts_quote_authorizations_quote_authorization_id",
                table: "posts",
                column: "quote_authorization_id",
                principalTable: "quote_authorizations",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_fediverse_actors_actor_id",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "fk_quote_authorizations_fediverse_actors_author_id",
                table: "quote_authorizations");

            migrationBuilder.DropForeignKey(
                name: "fk_quote_authorizations_posts_quote_post_id",
                table: "quote_authorizations");

            migrationBuilder.DropForeignKey(
                name: "fk_quote_authorizations_posts_target_post_id",
                table: "quote_authorizations");

            migrationBuilder.DropTable(
                name: "activity_pub_deliveries");

            migrationBuilder.DropTable(
                name: "automod_rules");

            migrationBuilder.DropTable(
                name: "boosts");

            migrationBuilder.DropTable(
                name: "delivery_dead_letters");

            migrationBuilder.DropTable(
                name: "discovery_preferences");

            migrationBuilder.DropTable(
                name: "fediverse_keys");

            migrationBuilder.DropTable(
                name: "fediverse_moderation_rules");

            migrationBuilder.DropTable(
                name: "fediverse_relationships");

            migrationBuilder.DropTable(
                name: "live_stream_awards");

            migrationBuilder.DropTable(
                name: "live_stream_chat_messages");

            migrationBuilder.DropTable(
                name: "poll_answers");

            migrationBuilder.DropTable(
                name: "poll_questions");

            migrationBuilder.DropTable(
                name: "post_awards");

            migrationBuilder.DropTable(
                name: "post_category_links");

            migrationBuilder.DropTable(
                name: "post_category_subscriptions");

            migrationBuilder.DropTable(
                name: "post_collection_links");

            migrationBuilder.DropTable(
                name: "post_featured_records");

            migrationBuilder.DropTable(
                name: "post_interest_profiles");

            migrationBuilder.DropTable(
                name: "post_reactions");

            migrationBuilder.DropTable(
                name: "post_tag_links");

            migrationBuilder.DropTable(
                name: "publisher_features");

            migrationBuilder.DropTable(
                name: "publisher_follow_requests");

            migrationBuilder.DropTable(
                name: "publisher_members");

            migrationBuilder.DropTable(
                name: "publisher_subscriptions");

            migrationBuilder.DropTable(
                name: "publishing_settings");

            migrationBuilder.DropTable(
                name: "sticker_pack_ownerships");

            migrationBuilder.DropTable(
                name: "stickers");

            migrationBuilder.DropTable(
                name: "live_streams");

            migrationBuilder.DropTable(
                name: "polls");

            migrationBuilder.DropTable(
                name: "post_categories");

            migrationBuilder.DropTable(
                name: "post_collections");

            migrationBuilder.DropTable(
                name: "post_tags");

            migrationBuilder.DropTable(
                name: "sticker_packs");

            migrationBuilder.DropTable(
                name: "fediverse_actors");

            migrationBuilder.DropTable(
                name: "fediverse_instances");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "quote_authorizations");
        }
    }
}
